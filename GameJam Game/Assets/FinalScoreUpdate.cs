using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class FinalScoreUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score;
    [SerializeField] private int count;
    private bool noMore = false;

    private void Start()
    {
        score = GameManager.Instance.GetScore();
        noMore = false;
    }

    private void Update()
    {
        if(score <= count) 
        {
            noMore = true;
        }

        if(!noMore)
        {
            count += 1;
            scoreText.text = count.ToString();
        }
    }
}
