using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knife : MonoBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private Rigidbody _rb;
    private float _lifeTime = 10f;

    private AudioClip _knifeHit;

    private void Start()
    {
        if(!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }

        if (_rb.velocity.magnitude < _minSpeed)
        {
            _rb.velocity = transform.forward * _minSpeed;
        }

        _knifeHit = GameManager.Instance.GetKnifeHitSFX();
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
            GameManager.Instance.AddToScore(100);
            AudioSource.PlayClipAtPoint(_knifeHit, transform.position);
            Destroy(gameObject);
        }
    }

    public Rigidbody GetRigidbody() => _rb;
}
