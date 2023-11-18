using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Start Signleton
    public static GameManager Instance;

    private float highScore = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //End Singleton
    }

    [SerializeField] private GameObject player;

    public GameObject GetPlayer() => player;

}
