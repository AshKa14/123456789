using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Pool<BulletController> m_BulletPool;
    private static Pool<AsteroidController> m_AsteroidPool;

    public static Pool<BulletController> BulletPool => m_BulletPool;


    private static int s_Lives = 3;
    private static int s_MaxLives = 5;
    private static int m_score = 0;
    private static int s_GainLifeScore = 1000;

    public static OnUpdateLives OnUpdateLives;
    public static OnUpdateScore OnUpdateScore;
    public GameObject m_BulletPrefab;

    public void Start()
    {
        m_BulletPool = new Pool<BulletController>(5, m_BulletPrefab);
    }

    public static int Lives
    {
        get { return s_Lives; }
        set
        {
            s_Lives = value;
            if (s_Lives > s_MaxLives)
            {
                s_Lives = s_MaxLives;
            }

            if (OnUpdateLives != null)
                OnUpdateLives(s_Lives);
        }
    }
    public static int Score
    {
        get { return m_score; }
        set
        {
            m_score = value;
            if (m_score > s_GainLifeScore)
            {
                Lives += 1;
                s_GainLifeScore *= 2;
            }

            if (OnUpdateScore != null)
                OnUpdateScore(m_score);
        }

    }
    public static void LoadScene(string sceneName)
    {
        OnUpdateLives = null;
        OnUpdateScore = null;

        if (sceneName == "Game Over")

            WriteScore();
        WriteScoreXML(Path.Combine(Application.dataPath, "score.xml"));

        SceneManager.LoadScene(sceneName);
    }

    private static void WriteScore()
    {


        // donde quiero escribir la informacion
        string filePath = Path.Combine(Application.dataPath, "scores.txt");

        if (File.Exists(filePath))
        {
            var fileStream = File.CreateText(filePath);
            // no escribimos nada porque solo estamos creando el archivo
            fileStream.Close();
        }

        //creo un stream de escritura para serializar el score
        StreamWriter fileWriter = File.AppendText(filePath);

        //escribo una linea de texto en el archivo de score
        fileWriter.WriteLine(m_score);

        // cierro el stream de escritura
        fileWriter.Close();

    }

    private static void WriteScoreXML(string filePath)
    {
        List<Score> savedScores = GetScores();

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Score>));

        using (var filestream = File.Create(filePath))
        {
            xmlSerializer.Serialize(filestream, savedScores);
        }
        /*
        FileStream fileStream = File.Create(filePath);
        XmlWriter xmlWriter = XmlWriter.Create(fileStream);

        xmlWriter.WriteStartDocument();

        xmlWriter.WriteStartElement("Score");

        for (int i = 0; i < savedScores.Count; i++)
        {
            xmlWriter.WriteElementString("score", "" + savedScores[i]);
        }
        

        xmlWriter.WriteEndElement();


        xmlWriter.Close();
        fileStream.Close();
        */
    }

    private static List<Score> GetScores()
    {
        List<Score> scores = new List<Score>();


        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Score>));

        using (var filestream = File.OpenRead(Path.Combine(Application.dataPath, "score.xml")))
        {
            scores = (List<Score>)xmlSerializer.Deserialize(filestream);
            foreach (var score in scores)
            {
                Debug.Log(score.name + ":" + score.score);
            }

        }

            /*
             var filePath = Path.Combine(Application.dataPath, "scores.txt");

             StreamReader streamReader = new StreamReader(filePath);

             string line = streamReader.ReadLine();
             while (string.IsNullOrEmpty(line))
             {
                 scores.Add(int.Parse(line));
                 line = streamReader.ReadLine();
             }
             scores.Sort();
             scores.Reverse();
            */
            return scores;

        }

    }


    public delegate void OnUpdateLives(int lives);

    public delegate void OnUpdateScore(float score);
