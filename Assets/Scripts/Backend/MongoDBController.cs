using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using UnityEngine;

using MongoDB.Driver.Linq;  
public class MongoDBController:MonoBehaviour,IDatabaseController
{
    public static MongoDBController Instance;
    //pwd = vzNDd41M5s3rwYBb
    private const string MONGO_URI = "mongodb://admin:vzNDd41M5s3rwYBb@barnabus-dev-shard-00-00.ecvnv.mongodb.net:27017,barnabus-dev-shard-00-01.ecvnv.mongodb.net:27017,barnabus-dev-shard-00-02.ecvnv.mongodb.net:27017/?ssl=true&replicaSet=atlas-65a93m-shard-0&authSource=admin&retryWrites=true&w=majority";
    //@barnabus-dev.ecvnv.mongodb.net/?retryWrites=true&w=majority";
    private const string DATABASE_NAME = "barnabus-dev";
    private const string ANALYTICS_COLLECTION_NAME = "analytics";
    private const string USER_COLLECTION_NAME = "user";
    

    private MongoClient client;
    private IMongoDatabase db;
    IMongoCollection<BsonDocument> userCollection;
    IMongoCollection<BsonDocument> analyticsCollection;
    IClientSessionHandle session;

    HeaderBuilder headerBuilder;
    private string sessionID;
    private double startSessionTimeUtc;
    private string startSessionTimeLocal;
    
    
    private void Awake() {
        SetSingle();
        InitDatabase();
        InitSession();
        BuildHeader();
    }
    void SetSingle(){
        if (Instance != null)
        {
            Destroy(this);
        }
        else    
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
    private void InitDatabase(){
        var settings = MongoClientSettings.FromConnectionString(MONGO_URI);
        //settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        client = new MongoClient(settings);
        
        db = client.GetDatabase(DATABASE_NAME);
        userCollection = db.GetCollection<BsonDocument>(USER_COLLECTION_NAME);
        analyticsCollection = db.GetCollection<BsonDocument>(ANALYTICS_COLLECTION_NAME);

    }
    private void InitSession(){
        session = client.StartSession();
        sessionID = session.ServerSession.Id["id"].ToString().Split(':')[1];
        startSessionTimeUtc = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        startSessionTimeLocal = System.DateTimeOffset.Now.ToString();
        
    }
    private void BuildHeader(){
        headerBuilder = new HeaderBuilder(this,sessionID);
        headerBuilder.UpdateTimestamp();
    }
    
    public void InsertEvent(string eventName,BsonDocument eventVal){
        headerBuilder.UpdateTimestamp();
        BsonDocument bsondoc = new BsonDocument();
        
        bsondoc.Add("eventName",eventName);
        bsondoc.AddRange(headerBuilder.GetHeader());
        bsondoc.AddRange(eventVal);
        

        analyticsCollection.InsertOneAsync(bsondoc);
        
        Debug.Log("[mongo]"+bsondoc.ToString());
    }
    public void SaveUserDatas(BsonDocument datas){
        headerBuilder.UpdateTimestamp();
        BsonDocument bsondoc = new BsonDocument();
        
        bsondoc.AddRange(headerBuilder.GetHeader());
        bsondoc.AddRange(datas);
        analyticsCollection.InsertOneAsync(bsondoc);
        //Debug.Log(datas);
    }
    /*
    public void CreateUser(string uid){
        UserDatas userDatas = new UserDatas();
        userDatas.userID = uid;
        userDatas.settings = new Settings();
        userDatas.attributes = new Attributes();
        userDatas.attributes.firstSessionTimestampUTC = startSessionTimeUtc;
        userDatas.attributes.firstSessionTimestampLocal = startSessionTimeLocal;
        userDatas.attributes.totalSessionCount = 1;
        BsonDocument userBson = userDatas.ToBsonDocument<UserDatas>();
        
        var filter = Builders<BsonDocument>.Filter.Eq("userID", uid);
        //var update = Builders<BsonDocument>.Update.Set("price", 52000);
        var updateOpt = new UpdateOptions();
        updateOpt.IsUpsert = true;
        analyticsCollection.UpdateOne(filter ,userBson );
        Debug.Log(userBson.ToString());
    }
    public void Select(){
        List<BsonDocument> userModelList = userCollection.Find(user => true).ToList();
        BsonDocument[] userAsap= userModelList.ToArray();
        foreach(BsonDocument asap in userAsap)
        {
            //print(asap);
        }
    }*/
    
}
