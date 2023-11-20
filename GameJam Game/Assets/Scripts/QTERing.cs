using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTERing : MonoBehaviour
{
    public float intialDelay = 1.25f;
    public float pressedDelay = 0.75f;

    private int qteCount;

    private Vector3 totalScale;
    private Vector3 startScale;

    [SerializeField] private GameObject innerRing;

    private void Awake()
    {
        totalScale = transform.localScale - innerRing.transform.localScale;
        startScale = transform.localScale;
        qteCount = 0;
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
            innerRing.SetActive(false);
        }

        if(GameManager.Instance.GetQTECount() == qteCount + 0)
        {
            Debug.Log("GRAH");
            intialDelay -= 0.05f;
            pressedDelay -= 0.05f;

            qteCount = GameManager.Instance.GetQTECount();
        }
    }
}
