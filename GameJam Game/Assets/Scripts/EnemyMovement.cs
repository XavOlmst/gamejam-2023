using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;

    //This is temporary
    private void Start()
    {
        _target = GameManager.Instance.GetPlayer().transform;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            Debug.Log("Reached the player, enable a QTE");
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target) => _target = target;
    public Transform GetTarget() => _target;
}
