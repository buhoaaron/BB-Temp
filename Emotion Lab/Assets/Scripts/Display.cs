using UnityEngine;
using TMPro;


public class Display : MonoBehaviour
{
    private DatabaseAccess databaseAccess;

    [SerializeField] private TMP_Text text;

    private void Start()
    {
        databaseAccess = GetComponent<DatabaseAccess>();
        DisplayHighScore();
    }

    private async void DisplayHighScore()
    {
        var task = databaseAccess.GetScoresFromDatabase();
        var result = await task;
        var output = "";
        foreach (var score in result)
        {
            output += score.UserName + " Score: " + score.Score + "\n";
        }
        text.text = output;
    }
}
