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

    private void Start()
    {
        _chosenKey = (KeyCode) _possibleKeys[Random.Range(0, _possibleKeys.Count)];
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
