using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://admin:admin@cluster0.zkasjve.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    private void Start()
    {
        database = client.GetDatabase("HighScoreDB");
        collection = database.GetCollection<BsonDocument>("HighScoreCollection");

        // Test Data
        // var document = new BsonDocument { { "username", 100 } };
        // collection.InsertOne(document);
    }

    public async void SaveScoreToDatabase(string userName, int score)
    {
        var document = new BsonDocument { { userName, score } };
        await collection.InsertOneAsync(document);
        // Next....
    }

    public async Task<List<HighScore>> GetScoresFromDatabase()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<HighScore> highScores = new List<HighScore>();
        foreach (var score in scoresAwaited.ToList())
        {

        }
    }
}

public class HighScore
{
    public string UserName { get; set; }
    public int Score { get; set; }
}
