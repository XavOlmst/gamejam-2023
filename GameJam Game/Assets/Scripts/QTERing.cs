using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTERing : MonoBehaviour
{
    public float intialDelay;
    public float pressedDelay;

    private Vector3 totalScale;
    private Vector3 startScale;

    [SerializeField] private GameObject innerRing;

    private void Awake()
    {
        totalScale = transform.localScale - innerRing.transform.localScale;
        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        
        if(transform.localScale.x >= totalScale.x || transform.localScale.y >= totalScale.y)
        {
            transform.localScale -= (totalScale / intialDelay) * Time.deltaTime;
        }
        else
        {
            transform.localScale -= (innerRing.transform.localScale / pressedDelay) * Time.deltaTime;
        }

        if(transform.localScale.x <= 0 || transform.localScale.y <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
