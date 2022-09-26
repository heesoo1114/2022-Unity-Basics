using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private SpawnModes spawnMode = SpawnModes.Fixed;

    [SerializeField]
    private int enemyCount = 10;

    [SerializeField]
    private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField] private float fixedTimer; // = delayBtnSpawn
    [SerializeField] private float _delayBtwSpwans;

    [Header("Random Delay")]
    [SerializeField]
    private float minRandomDelay;
    [SerializeField]
    private float maxRandomDelay;

    // 풀러 선언
    private ObjPooler _pooler;
    private WayPoint _waypoint;

    private float _spawnTimer;
    private float _enemiesSpawned;
    private int _enemiesRemaining;

    private void Start()
    {
        _pooler = GetComponent<ObjPooler>();
        _waypoint = GetComponent<WayPoint>();

        _enemiesRemaining = enemyCount; // 10개
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer < 0)
        {
            _spawnTimer = GetRandomDelay();
            if(_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.waypoint = _waypoint;
        // Instantiate(testGO, transform.position, Quaternion.identity);

        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;

        newInstance.SetActive(true);
    }

    private float GetRandomDelay()
    {
        if(spawnMode == SpawnModes.Random)
        {
            float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
            return randomTimer;
        }
        else
        {
            float timer = fixedTimer;
            return timer;
        }
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(_delayBtwSpwans);
        _enemiesSpawned = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRemaining--;
        if(_enemiesRemaining <= 0)
        {
            // 새 wave 시작
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }
}
