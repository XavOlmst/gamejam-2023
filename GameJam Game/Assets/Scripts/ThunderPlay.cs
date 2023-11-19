using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPlay : MonoBehaviour
{
    private AudioClip thunder1;
    private AudioClip thunder2;
    private AudioClip thunder3;

    [SerializeField] private AudioSource thunderSource;

    private int randomNum = 0;

    private void Start()
    {
        ThunderRandom();
        StartCoroutine(waitLightning());

        thunder1 = GameManager.Instance.GetThunder1SFX();
        thunder2 = GameManager.Instance.GetThunder2SFX();
        thunder3 = GameManager.Instance.GetThunder3SFX();
    }

    private void ThunderRandom()
    {
        randomNum = Random.Range(20, 85);
        
    }

    private IEnumerator waitLightning()
    {
        Debug.Log(randomNum);
        yield return new WaitForSeconds(randomNum);

        int thunderNum = Random.Range(1, 4);
        switch(thunderNum)
        {
            case 1:
                Debug.Log(thunder1);
                thunderSource.clip = thunder1;
                break;
            case 2:
                Debug.Log(thunder2);
                thunderSource.clip = thunder2;
                break;
            case 3:
                Debug.Log(thunder3);
                thunderSource.clip = thunder3;
                break;
        }

        thunderSource.Play();
        ThunderRandom();
        StartCoroutine(waitLightning());
    }
}
