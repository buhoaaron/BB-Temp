using MongoDB.Bson;
interface IDatabaseController
{
    public void InsertEvent(string eventName,BsonDocument eventVal);
    public void SaveUserDatas(BsonDocument datas);
}