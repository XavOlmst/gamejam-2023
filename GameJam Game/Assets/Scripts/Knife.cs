using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knife : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    private float _lifeTime = 10f;

    private void Start()
    {
        if(!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }

        _rb.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if(_lifeTime < 0)
        {
            Destroy(gameObject);   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameManager.Instance.AddToScore(100);
        }
    }
}
