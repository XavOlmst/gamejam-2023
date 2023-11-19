using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashDisplay : MonoBehaviour
{
    [SerializeField] private float _timeToComplete = 5;
    [SerializeField] private int _keyPresses = 10;

    [SerializeField] private Image _timeVisuals;
    [SerializeField] private Image _pressVisuals;

    private float _fillPerPress;
    private float _timeFillRate;
    private void OnEnable()
    {
        _pressVisuals.fillAmount = 0;
        _timeVisuals.fillAmount = 0;

        _timeFillRate = 1 / _timeToComplete;

        if (_keyPresses != 0)
        {
            _fillPerPress = 1 / _keyPresses;
        }

        _keyPresses = 0;
    }

    private void Update()
    {
        _timeVisuals.fillAmount += _timeFillRate * Time.deltaTime;
    }

    public void AddKeyPress()
    {
        _keyPresses++;
        _pressVisuals.fillAmount = _keyPresses * _fillPerPress;
    }

    public void SetKeyPresses(int keyPresses)
    {
        _fillPerPress = 1.0f / keyPresses;
        _keyPresses = 0;
    }

    public void SetTimeToComplete(float time)
    {
        _timeToComplete = time;
        _timeFillRate = 1 / _timeToComplete;
    }
}
