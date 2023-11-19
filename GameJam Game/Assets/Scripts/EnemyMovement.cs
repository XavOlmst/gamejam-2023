using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _numAnimations;
    [SerializeField] private float _moveSpeed;
    private float _animationChangeTimer;

    [SerializeField] private GameObject _qteElement;

    //This is temporary
    private void Start()
    {
        _target = GameManager.Instance.GetPlayer().transform;

        _animationChangeTimer = Random.Range(1, 3f);
    }

    private void Update()
    {
        if (GameManager.Instance.IsQTEActive()) return;


        transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _target.position) < 1f)
        {
            Debug.Log("Reached the player, enable a QTE");
            Instantiate(GameManager.Instance.GetTimingQTE(), transform);
            _target.LookAt(transform);
            //Destroy(gameObject);
        }


        _animationChangeTimer -= Time.deltaTime;

        if (_animationChangeTimer < 0 && _animator != null)
        {
            _animator.SetInteger("animationIndex", Random.Range(0, _numAnimations));
            _animationChangeTimer = Random.Range(1, 3f);
        }
    }

    public void SetTarget(Transform target) => _target = target;
    public Transform GetTarget() => _target;
}
