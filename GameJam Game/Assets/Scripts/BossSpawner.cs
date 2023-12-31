using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private AudioSource _bossMusic;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private float minTime = 30f;
    [SerializeField] private float maxTime = 60f;
    private float _bossTimer;

    private void Start()
    {
        _bossTimer = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if (GameManager.Instance.IsQTEActive()) return;


        _bossTimer -= Time.deltaTime;

        if(_bossTimer < 0)
        {
            Instantiate(_bossPrefab, _spawnLocation.position, Quaternion.identity);
            _bossTimer = Random.Range(minTime, maxTime);

            GameManager.Instance.GetMusicManager().ChangeSong(_bossMusic, 5);
            GameManager.Instance.GetEnemySpawner().ClearEnemies();
        }

    }

}
