using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStore : MonoBehaviour
{
    public static ScoreStore instance;
    public int score;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void StoreScore()
    {
        score = GameManager.Instance.GetScore();
    }

    public int GetScoreFromThing() => score;
}
