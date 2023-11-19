using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GoodKeyCodes
{
    Q = KeyCode.Q,
    W = KeyCode.W,
    E = KeyCode.E,
    R = KeyCode.R,
    A = KeyCode.A,
    S = KeyCode.S,
    D = KeyCode.D,
    F = KeyCode.F, 
    Z = KeyCode.Z,
    X = KeyCode.X,
    C = KeyCode.C,
    V = KeyCode.V, 
}

public class ButtonMashQTE : MonoBehaviour
{
    [SerializeField] private List<GoodKeyCodes> _possibleKeys;
    [SerializeField] private int _pressesNeeded = 10;
    [SerializeField] private float _timeToComplete = 5;
    private KeyCode _chosenKey;
    private int _timesPressed = 0;
    private bool _passedQTE = false;

    private AudioClip _zapDeath;
    private AudioSource audioPlayer;

    private void Start()
    {
        _chosenKey = (KeyCode) _possibleKeys[Random.Range(0, _possibleKeys.Count)];
        GameManager.Instance.GetQTECanvas().SetActive(true);
        GameManager.Instance.GetQTEText().enabled = true;
        GameManager.Instance.GetQTEText().text = _chosenKey.ToString();
        GameManager.Instance.SetQTEState(true);

        Debug.Log($"QTE Key: {_chosenKey}");
        _zapDeath = GameManager.Instance.GetDeathZapSFX();

        audioPlayer = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_chosenKey) && !_passedQTE)
        {
            _timesPressed++;

            if(_timesPressed >= _pressesNeeded && _timeToComplete >= 0)
            {
                Debug.Log("Passed Button Mash QTE");
                _passedQTE = true;
                FinishQTE();
            }
        }

        _timeToComplete -= Time.deltaTime;

        if(_timeToComplete < 0 && _timesPressed < _pressesNeeded && !_passedQTE)
        {
            Debug.Log("Failed Button Mash QTE");
            audioPlayer.clip = _zapDeath;
            audioPlayer.Play();
            FinishQTE();
        }
    }

    public void FinishQTE()
    {
        GameManager.Instance.GetQTECanvas().SetActive(false);
        GameManager.Instance.GetQTEText().enabled = false;
        GameManager.Instance.SetQTEState(false);

        Destroy(transform.parent.gameObject);
    }
}
