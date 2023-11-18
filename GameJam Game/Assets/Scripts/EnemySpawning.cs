using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    //TODO: Change to enemy class when we get enemy class
    [SerializeField] private TimelineManager _moveManager;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDistance = 25f;
    [SerializeField] private float minSpawnDelay = 10;
    [SerializeField] private float maxSpawnDelay = 20;
    [SerializeField] private Vector2Int numberSpawnedRange;
    private List<GameObject> _enemiesSpawned = new();
    private float spawnTimer = 0f;

    private void Start()
    {
        //spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0)
        {
            spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay);

            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        int numEnemiesToSpawn = Random.Range(numberSpawnedRange.x, numberSpawnedRange.y);

        for(int i = 0; i < numEnemiesToSpawn; i++)
        {
            Vector3 spawnPoint = Random.onUnitSphere;

            float playerZ = GameManager.Instance.GetPlayer().transform.position.z;

            while (spawnPoint.z * _spawnDistance < playerZ || Mathf.Abs(spawnPoint.y) > 0.60)
            {
                spawnPoint = Random.onUnitSphere;
            }

            GameObject enemy = Instantiate(_enemyPrefab, spawnPoint * _spawnDistance, Quaternion.identity, transform);
            _enemiesSpawned.Add(enemy);
            //enemy.GetComponent<EnemyMovement>().SetTarget(_moveManager.GetRandomTimelineTransform());
        }
    }
}
