using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Start Signleton
    public static GameManager Instance;

    [SerializeField] private GameObject QTECanvas;
    [SerializeField] private TMP_Text qteText;
    [SerializeField] private GameObject _timingQTE;
    [SerializeField] private GameObject _mashQTE;
    [SerializeField] private MusicManager _musicManager;
    [SerializeField] private AudioSource _gameMusic;

    [SerializeField] private GameObject player;

    private bool _qteActive = false;
    private int highScore = 0;
    private int score = 0;

    [Header("AudioClips")]
    [SerializeField] AudioClip knifeThrow;

    [SerializeField] AudioClip knifeHit;

    [SerializeField] AudioClip miniDragonRoar;

    [SerializeField] AudioClip qteStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //End Singleton
    }

    public GameObject GetPlayer() => player;
    public GameObject GetQTECanvas() => QTECanvas;
    public TMP_Text GetQTEText() => qteText;
    public GameObject GetTimingQTE() => _timingQTE;
    public GameObject GetMashingQTE() => _timingQTE;
    public void SetQTEState(bool isQTEActive) => _qteActive = isQTEActive;
    public bool IsQTEActive() => _qteActive;
    public MusicManager GetMusicManager() => _musicManager;
    public AudioSource GetGameMusic() => _gameMusic;
    public int GetHighScore() => highScore;

    public void SetHighScore(int highScore) => this.highScore = highScore;

    public int GetScore() => score;

    public void AddToScore(int scoreToAdd) => score += scoreToAdd;

    public static void SaveScoreToFile<T>(List<T> toSave, string fileName)
    {
        Debug.Log(GetPath(fileName));
        string fileContent = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(fileName), fileContent);
    }

    public static List<T> ReadScoreFromFileList<T>(string fileName)
    {
        string fileContent = ReadFile(GetPath(fileName));
        
        if(string.IsNullOrEmpty(fileContent) || fileContent == "{}")
        {
            return new List<T>();
        }

        List<T> Res = JsonHelper.FromJson<T>(fileContent).ToList();
        return Res;
    }

    private static string GetPath(string getFileName)
    {
        return Application.persistentDataPath + "/" + getFileName;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    
    private static string ReadFile(string path)
    {
        if(File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path)) 
            {
                string fileContent = reader.ReadToEnd();
                return fileContent;
            }
        }
        return "";
    }

    public AudioClip GetKnifeThrowSFX() => knifeThrow;
    public AudioClip GetKnifeHitSFX() => knifeHit;
    public AudioClip GetMiniDragonRoarSFX() => miniDragonRoar;
    public AudioClip GetQTEStartSFX() => qteStart;

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }

    }
}
