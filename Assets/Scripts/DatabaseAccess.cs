using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://admin:admin@cluster0.zkasjve.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    private void Awake()
    {
        database = client.GetDatabase("HighScoreDB");
        collection = database.GetCollection<BsonDocument>("HighScoreCollection");
        Debug.Log(collection);

        // Test Data;
        // var document = new BsonDocument { { "username", 1000 } };
        // collection.InsertOne(document);
    }

    private void Start()
    {
        GetScoresFromDatabase();
    }

    public async void SaveScoreToDatabase(string userName, int score)
    {
        var document = new BsonDocument { { userName, score } };
        await collection.InsertOneAsync(document);
    }

    public async Task<List<HighScore>> GetScoresFromDatabase()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<HighScore> highScores = new List<HighScore>();
        foreach (var score in scoresAwaited.ToList())
        {
            highScores.Add(Deserialize(score.ToString()));
        }

        highScores = highScores.OrderByDescending(p => p.Score).ToList();

        return highScores;
    }

    private HighScore Deserialize(string rawJason)
    {
        // { "_id" : ObjectId("63a20e00e39d4852830a8f3b"), "d" : 700 }
        var highScore = new HighScore();
        var stringWithoutID = rawJason.Substring(rawJason.IndexOf("),") + 4);
        var userName = stringWithoutID.Substring(0, stringWithoutID.IndexOf(":") - 2);
        var score = stringWithoutID.Substring(stringWithoutID.IndexOf(":") + 2, stringWithoutID.IndexOf("}") - stringWithoutID.IndexOf(":") - 3);
        highScore.UserName = userName;
        highScore.Score = Convert.ToInt32(score);
        return highScore;
    }
}

public class HighScore
{
    public string UserName { get; set; }
    public int Score { get; set; }
}
