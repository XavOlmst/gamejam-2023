using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private MashDisplay _display;

    private AudioClip _zapDeath;
    [SerializeField] private AudioSource audioPlayer;

    private void Start()
    {
        _chosenKey = (KeyCode) _possibleKeys[Random.Range(0, _possibleKeys.Count)];
        GameManager.Instance.GetMashQTECanvas().SetActive(true);
        GameManager.Instance.GetMashQTEText().enabled = true;
        GameManager.Instance.GetMashQTEText().text = _chosenKey.ToString();
        GameManager.Instance.SetQTEState(true);

        _display = GameManager.Instance.GetMashQTECanvas().GetComponent<MashDisplay>();
        _display.SetKeyPresses(_pressesNeeded);
        _display.SetTimeToComplete(_timeToComplete);

        Debug.Log($"QTE Key: {_chosenKey}");
        _zapDeath = GameManager.Instance.GetDeathZapSFX();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_chosenKey) && !_passedQTE)
        {
            _timesPressed++;
            _display.AddKeyPress();

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
            _passedQTE = true;
            StartCoroutine(WaitforDeathSound());
        }
    }

    public void FinishQTE()
    {
        GameManager.Instance.GetMashQTECanvas().SetActive(false);
        GameManager.Instance.GetMashQTEText().enabled = false;
        GameManager.Instance.SetQTEState(false);

        Destroy(transform.parent.gameObject);
    }

    private IEnumerator WaitforDeathSound()
    {
        audioPlayer.Stop();
        audioPlayer.clip = _zapDeath;
        AudioSource.PlayClipAtPoint(_zapDeath, GameManager.Instance.GetPlayer().transform.position);

        Debug.Log(audioPlayer.clip);
        yield return new WaitForSeconds(_zapDeath.length);

        FinishQTE();
        GameManager.Instance.SetQTEState(true);
        SceneManager.LoadScene("LoseScene");
    }
}
