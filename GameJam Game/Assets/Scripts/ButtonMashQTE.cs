using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonMashQTE : MonoBehaviour
{
    [SerializeField] private List<KeyCode> _possibleKeys;
    [SerializeField] private int _pressesNeeded = 10;
    [SerializeField] private float _timeToComplete = 5;
    private KeyCode _chosenKey;
    private int _timesPressed = 0;
    private bool _passedQTE = false;

    private void Start()
    {
        _chosenKey = _possibleKeys[Random.Range(0, _possibleKeys.Count)];
        Debug.Log($"QTE Key: {_chosenKey}");
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
            }
        }

        _timeToComplete -= Time.deltaTime;

        if(_timeToComplete < 0 && _timesPressed < _pressesNeeded && !_passedQTE)
        {
            Debug.Log("Failed Button Mash QTE");
        }
    }
}
