using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class GameSettings 
{
    private GameSettings s_Instance;
    
    [MenuItem("Game Settings/Difficulty/Easy", false, 1)]
    public static void SetEasySettings()
    {
        var settings = new Settings
        {
            shipSpeed = 15,
            asteroidSpeed = new List<float> { 10f, 10f, 10f }
        };

        Debug.Log(settings.ToString());
        //SerializeJson(settings)

        using (StreamWriter streamWriter = File.CreateText(Path.Combine(Application.dataPath, "Settings", "GameSettings.json")))
        {
            streamWriter.WriteLine(settings.ToString());
        }
    }

}
