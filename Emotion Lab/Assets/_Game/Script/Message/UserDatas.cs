using UnityEngine;
public struct UserDatas
{
    public string userID;
    public Settings settings;
    public Attributes attributes;
    public Potion[] potions;
    public EmotionBuddie[] emotionBuddies;
    public SmallGame[] games;
}
public class Settings{
    public int soundEffect = 1;
    public int backgroundMusic = 1 ;
    public int voiceOver = 1;
    public string language = "en";
}   
public class Attributes{
    public double firstSessionTimestampUTC;
    public string firstSessionTimestampLocal;
    public double totalPlayTimeSec;
    public int totalSessionCount;
    public string[] gameVersions;
    int introVideoPlayCount;
    public Attributes(){
        
        totalPlayTimeSec = 0;
        totalSessionCount = 0;
        gameVersions = new string[1]{Application.version};
        introVideoPlayCount = 0;
    }
}
public class Potion
{
    public int id;
    public string color;
    public int qtyCurrent;
    public int qtyLifetime;
}
public class EmotionBuddie{
    public int id;
    public string name;
    public long hatchTimestampUTC;
    public int level;
}
public class SmallGame{
    public int id;
    public string name;
}
