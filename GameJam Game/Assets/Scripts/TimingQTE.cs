using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimingQTE : MonoBehaviour
{
    [SerializeField] private List<GoodKeyCodes> _possibleKeys;
    [SerializeField] private float _delayToPress = 1.25f;
    [SerializeField] private float _timeToPress = 0.75f;

    private AudioClip _qteStart;
    private AudioClip _chompDeath;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _qteElement;

    private KeyCode _chosenKey;
    private bool _passedQTE = false;

    [SerializeField] private AudioSource audioPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _chosenKey = (KeyCode)_possibleKeys[Random.Range(0, _possibleKeys.Count)];
        GameManager.Instance.GetTimingQTECanvas().SetActive(true);
        GameManager.Instance.GetTimingQTEText().enabled = true;
        GameManager.Instance.GetTimingQTEText().text = _chosenKey.ToString();
        GameManager.Instance.SetQTEState(true);
        Debug.Log($"QTE Key: {_chosenKey}");

        _qteStart = GameManager.Instance.GetQTEStartSFX();
        _chompDeath = GameManager.Instance.GetDeathChompSFX();

        QTERing ring = GameManager.Instance.GetTimingQTECanvas().GetComponentInChildren<QTERing>();

        ring.intialDelay = _delayToPress;
        ring.pressedDelay = _timeToPress;

        audioPlayer.clip = _qteStart;
        audioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !_passedQTE)
        {
            if(Input.GetKeyDown(_chosenKey) && _delayToPress < 0 && _timeToPress > 0)
            {
                Debug.Log("Passed Timing QTE");
                _passedQTE = true;
                FinishQTE();
            }
            else
            {
                Debug.Log("Failed Timing QTE");
                GameManager.Instance.GetTimingQTECanvas().SetActive(false);
                _passedQTE = true;
                StartCoroutine(WaitforDeathSound());
            }
        }

        _delayToPress -= Time.deltaTime;

        if(_delayToPress < 0 && !_passedQTE)
        {
            _timeToPress -= Time.deltaTime;

            Debug.Log($"Time left to press: {_timeToPress}");

            if(_timeToPress < 0)
            {
                Debug.Log("Failed Timing QTE");
                _passedQTE = true;
                StartCoroutine(WaitforDeathSound());
            }
        }
    }

    public void FinishQTE()
    {
        GameManager.Instance.GetTimingQTECanvas().SetActive(false);
        GameManager.Instance.GetTimingQTEText().enabled = false;
        GameManager.Instance.SetQTEState(false);

        Destroy(transform.parent.gameObject);
    }

    private IEnumerator WaitforDeathSound() 
    {
        audioPlayer.Stop();
        audioPlayer.clip = _chompDeath;
        AudioSource.PlayClipAtPoint(_chompDeath, GameManager.Instance.GetPlayer().transform.position);

        Debug.Log(audioPlayer.clip);
        yield return new WaitForSeconds(_chompDeath.length);

        FinishQTE();
        GameManager.Instance.SetQTEState(true);
        SceneManager.LoadScene("LoseScene");
    }
}
