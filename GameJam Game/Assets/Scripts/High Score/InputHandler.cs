using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] string fileName;

    List<InputEntry> entries = new List<InputEntry>();
    [SerializeField] int maxCount = 1;

    public delegate void OnHighScoreListChanged(List<InputEntry> list);
    public static event OnHighScoreListChanged OnListChanged;

    private void Start()
    {
        LoadHighScores();
    }

    public void StopGame()
    {
        AddHighScore(new InputEntry(nameInput.text, GameManager.Instance.GetScore()));
        nameInput.text = "Insert Name";
    }

    private void LoadHighScores()
    {
        entries = GameManager.ReadScoreFromFileList<InputEntry>(fileName);
        while(entries.Count > maxCount) 
        {
            entries.RemoveAt(maxCount);
        }

        if(OnListChanged != null)
        {
            OnListChanged.Invoke(entries);
        }
    }
    
    private void SaveHighScores()
    {
        GameManager.SaveScoreToFile<InputEntry>(entries, fileName);
    }

    public void AddHighScore(InputEntry scoreEntry)
    {
        for(int i = 0; i < maxCount; i++)
        { 
            if(i >= entries.Count || scoreEntry.highScore >= entries[i].highScore)
            {
                entries.Insert(i, scoreEntry);

                while(entries.Count > maxCount)
                {
                    entries.RemoveAt(maxCount);
                }

                SaveHighScores();

                if (OnListChanged != null)
                {
                    OnListChanged.Invoke(entries);
                }

                break;
            }
        }
    }
}
