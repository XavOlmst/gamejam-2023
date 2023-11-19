using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knife : MonoBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private Rigidbody _rb;
    private float _lifeTime = 10f;

    [SerializeField] private AudioSource _audioSource;

    private AudioClip _knifeHit;
    private AudioClip _knifeSwoosh;

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
        _knifeSwoosh = GameManager.Instance.GetKnifeThrowSFX();


        _audioSource.clip = _knifeSwoosh;
        _audioSource.loop = true;
        _audioSource.Play();
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
            AudioSource.PlayClipAtPoint(_knifeHit, transform.position);
            GameManager.Instance.AddToScore(100);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public Rigidbody GetRigidbody() => _rb;
}
