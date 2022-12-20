using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using MongoDB.Bson;
using UnityEngine.SceneManagement;
public class MessageBroadcaster : MonoBehaviour
{
    static MessageBroadcaster instance;
    IDatabaseController[] databaseController;
    // ButtonEventBuilder buttonEventBuilder;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        databaseController = GetComponents<IDatabaseController>();
    }
    private void Insert(string eventName, BsonDocument eventVal)
    {
        foreach (var database in databaseController)
        {
            database.InsertEvent(eventName, eventVal);
        }

    }
}