using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentScoreUI : MonoBehaviour
{
    public TMP_Text CurrentScore;
    public int Score;

    // Update is called once per frame
    void Update()
    {
        CurrentScore.text = GameManager.Instance.GetScore().ToString();
    }
}
