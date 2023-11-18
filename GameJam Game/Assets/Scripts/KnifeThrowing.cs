using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowing : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Animator _knifeAnimator;

    [Tooltip("Make negative to zoom in")]
    [SerializeField] private float _fovOffset;
    
    [SerializeField] private Knife _knife;
    [SerializeField] private float _maxThrowForce = 400f;
    [SerializeField] private float _maxHoldTime = 2f;
    private float _holdTimer;
    private float _initFOV;

    private void Start()
    {
        _initFOV = _camera.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_holdTimer < _maxHoldTime)
            {
                _holdTimer += Time.deltaTime;
                float heldPercent = _holdTimer / _maxHoldTime;

                if (heldPercent > 0.1f)
                {
                    _knifeAnimator.SetBool("KnifeCharge", true);
                    _camera.fieldOfView = _initFOV + (_fovOffset * heldPercent);
                }
            }

            return;
        }

        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _initFOV, 0.2f);

        if (Input.GetMouseButtonUp(0))
        {
            float heldPercent = _holdTimer / _maxHoldTime;
            Transform playerTransform = GameManager.Instance.GetPlayer().transform;

            Knife knife = Instantiate(_knife, playerTransform.position + playerTransform.forward, playerTransform.rotation);

            knife.GetRigidbody().AddForce(playerTransform.forward * (_maxThrowForce * heldPercent));
            _holdTimer = 0;

            _knifeAnimator.SetBool("KnifeCharge", false);
        }
    }

}
