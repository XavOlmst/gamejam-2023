using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnDestroy()
    {
        Debug.Log("Changing music back to normal music");
        GameManager.Instance.GetMusicManager().ChangeSong(GameManager.Instance.GetGameMusic(), 5f);
    }
}
