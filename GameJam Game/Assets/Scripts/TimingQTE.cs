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
        GameManager.Instance.GetQTECanvas().SetActive(true);
        GameManager.Instance.GetQTEText().enabled = true;
        GameManager.Instance.GetQTEText().text = _chosenKey.ToString();
        GameManager.Instance.SetQTEState(true);
        Debug.Log($"QTE Key: {_chosenKey}");

        _qteStart = GameManager.Instance.GetQTEStartSFX();
        _chompDeath = GameManager.Instance.GetDeathChompSFX();

        audioPlayer.clip = _qteStart;
        audioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_chosenKey) && !_passedQTE)
        {
            if(_delayToPress < 0 && _timeToPress > 0)
            {
                Debug.Log("Passed Timing QTE");
                _passedQTE = true;
                FinishQTE();
            }
            else
            {
                Debug.Log("Failed Timing QTE");
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
                StartCoroutine(WaitforDeathSound());
            }
        }
    }

    public void FinishQTE()
    {
        GameManager.Instance.GetQTECanvas().SetActive(false);
        GameManager.Instance.GetQTEText().enabled = false;
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
        SceneManager.LoadScene("LoseScene");
    }
}
