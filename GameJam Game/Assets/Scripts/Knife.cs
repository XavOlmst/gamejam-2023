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

    private int scoreSave;
    private int scoreMultiply = 1;

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
        if(!GameManager.Instance.IsQTEActive())
        {
            _rb.isKinematic = false;
        }
        else
        {
            _rb.isKinematic = true;
        }

        _lifeTime -= Time.deltaTime;

        if(_lifeTime < 0)
        {
            Destroy(gameObject);   
        }

        if(GameManager.Instance.GetScore() == scoreSave + 10000) 
        {
            scoreMultiply += 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(_knifeHit, transform.position);
            GameManager.Instance.AddToScore(100 * scoreMultiply);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public Rigidbody GetRigidbody() => _rb;
}
