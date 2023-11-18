using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimingQTE : MonoBehaviour
{
    [SerializeField] private List<GoodKeyCodes> _possibleKeys;
    [SerializeField] private float _delayToPress = 1.25f;
    [SerializeField] private float _timeToPress = 0.75f;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _qteElement;

    private KeyCode _chosenKey;
    private bool _passedQTE = false;

    // Start is called before the first frame update
    void Start()
    {
        _chosenKey = (KeyCode)_possibleKeys[Random.Range(0, _possibleKeys.Count)];
        GameManager.Instance.GetQTECanvas().SetActive(true);
        GameManager.Instance.GetQTEText().enabled = true;
        GameManager.Instance.GetQTEText().text = _chosenKey.ToString();
        GameManager.Instance.SetQTEState(true);
        Debug.Log($"QTE Key: {_chosenKey}");
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
                FinishQTE();
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
                FinishQTE();
            }
        }
    }

    public void FinishQTE()
    {
        GameManager.Instance.GetQTECanvas().SetActive(false);
        GameManager.Instance.GetQTEText().enabled = false;
        GameManager.Instance.SetQTEState(false);

        Destroy(gameObject);
    }
}
