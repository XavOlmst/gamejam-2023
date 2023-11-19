using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ButtonMashQTE _mashQTE;


    private void Start()
    {
        if(GameManager.Instance.IsQTEActive())
        {
            GameManager game = GameManager.Instance;

            game.GetTimingQTECanvas().SetActive(false);
            game.GetTimingQTEText().enabled = false;
        }

        GameManager.Instance.GetPlayer().transform.LookAt(transform);
        Instantiate(_mashQTE, transform);
    }

    private void OnDestroy()
    {
        Debug.Log("Changing music back to normal music");
        GameManager.Instance.GetMusicManager().ChangeSong(GameManager.Instance.GetGameMusic(), 5f);
    }
}
