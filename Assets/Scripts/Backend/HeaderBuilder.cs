using MongoDB.Bson;
using UnityEngine;
using System.Collections;
using SystemInfo = UnityEngine.Device.SystemInfo;
using Application = UnityEngine.Device.Application;
using UnityEngine.Networking;
public class HeaderBuilder
{
    private Header header;
    public HeaderBuilder(MonoBehaviour mono, string sessionID)
    {
        header = new Header();
        BuildDeviceInfo();
        header.sessionID = sessionID;
    }
    public void BuildDeviceInfo()
    {
        // header.unrecognizedPlayerID = Barnabus.DataManager.PlayerID;
        header.Model = SystemInfo.deviceModel;
        header.deviceOsVersion = SystemInfo.operatingSystem;
        header.gameVersion = Application.version;
        header.deviceScreenSize = new int[2] { Screen.width, Screen.height };
    }
    public void UpdateTimestamp()
    {
        // header.unrecognizedPlayerID = Barnabus.DataManager.PlayerID;
        header.eventTimestampUTC = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        header.eventTimestampLocal = System.DateTimeOffset.Now.ToString();
    }
    public void UpdateSession(string sessionID)
    {
        header.sessionID = sessionID;
    }
    public BsonDocument GetHeader()
    {
        return header.ToBsonDocument<Header>();
    }
}
public struct Header
{
    public string unrecognizedPlayerID { get; set; }
    public long eventTimestampUTC { get; set; }
    public string eventTimestampLocal { get; set; }
    public string Model { get; set; }
    public string deviceOsVersion { get; set; }
    public string deviceGeoLocation { get; set; }
    public string deviceRegion { get; set; }
    public string gameVersion { get; set; }
    public string sessionID { get; set; }
    public int[] deviceScreenSize { get; set; }
}