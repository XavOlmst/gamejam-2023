using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowing : MonoBehaviour
{
    [SerializeField] private GameObject _knife;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Transform playerTransform = GameManager.Instance.GetPlayer().transform;
            Instantiate(_knife, playerTransform.position + playerTransform.forward, playerTransform.rotation);
        }
    }

}
