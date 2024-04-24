using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Scores:");

        string path = Path.Combine(Application.dataPath, "scores.txt");

        StreamReader streamReader = new StreamReader(path);

        // Create a list to store the scores
       // List<string> scoretext = new List<string>();
        List<int> scores = new List<int>();

        string line = streamReader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            // Convert the score from string to int and add it to the list

            scores.Add(int.Parse(line));
            line = streamReader.ReadLine();
        }

        // Sort the scores in descending order
        scores.Sort();
        scores.Reverse();

        // Iterate through the sorted scores and print them to the console
        foreach (var score in scores)
        {
            Debug.Log(score);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()

    {
        GameManager.Lives = 3;
        GameManager.Score = 0;
        SceneManager.LoadScene("Asteroids 2022 v2 main");
        
    }
}
