using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowing : MonoBehaviour
{
    [SerializeField] private Knife _knife;
    [SerializeField] private float _maxThrowForce = 400f;
    [SerializeField] private float _maxHoldTime = 2f;
    private float _holdTimer;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_holdTimer < _maxHoldTime)
            {
                _holdTimer += Time.deltaTime;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            float heldPercent = _holdTimer / _maxHoldTime;
            Transform playerTransform = GameManager.Instance.GetPlayer().transform;

            Knife knife = Instantiate(_knife, playerTransform.position + playerTransform.forward, playerTransform.rotation);

            knife.GetRigidbody().AddForce(playerTransform.forward * (_maxThrowForce * heldPercent));
            _holdTimer = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {

        }
    }

}
