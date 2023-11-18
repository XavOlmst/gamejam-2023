using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingQTE : MonoBehaviour
{
    [SerializeField] private List<GoodKeyCodes> _possibleKeys;
    [SerializeField] private float _delayToPress = 1.25f;
    [SerializeField] private float _timeToPress = 0.75f;
    private KeyCode _chosenKey;
    private bool _passedQTE = false;

    // Start is called before the first frame update
    void Start()
    {
        _chosenKey = _possibleKeys[Random.Range(0, _possibleKeys.Count)];
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
            }
            else
            {
                Debug.Log("Failed Timing QTE");
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
            }
        }
    }
}
