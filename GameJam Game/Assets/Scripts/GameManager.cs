using System.Collections;
using System.Collections.Generic;
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

    private bool _qteActive = false;
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
    public GameObject GetQTECanvas() => QTECanvas;
    public TMP_Text GetQTEText() => qteText;
    public GameObject GetTimingQTE() => _timingQTE;
    public GameObject GetMashingQTE() => _timingQTE;
    public void SetQTEState(bool isQTEActive) => _qteActive = isQTEActive;
    public bool IsQTEActive() => _qteActive;

}
