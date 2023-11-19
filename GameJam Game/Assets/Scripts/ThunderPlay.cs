using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPlay : MonoBehaviour
{
    private AudioClip thunder1;
    private AudioClip thunder2;
    private AudioClip thunder3;

    [SerializeField] private AudioSource thunderSource;

    private void Start()
    {
        StartCoroutine(waitLightning());

        thunder1 = GameManager.Instance.GetThunder1SFX();
        thunder2 = GameManager.Instance.GetThunder2SFX();
        thunder3 = GameManager.Instance.GetThunder3SFX();
    }

    private IEnumerator waitLightning()
    {
        var randomNum = Random.Range(20, 85);
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
        StartCoroutine(waitLightning());
    }
}
