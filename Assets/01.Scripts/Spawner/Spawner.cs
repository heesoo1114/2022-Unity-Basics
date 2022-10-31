using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Fixed,
    Random
}
public class spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField]
    private int enemyCount = 10;

    [SerializeField] private float delayBtwWaves = 1f;
    

    [Header("Fixed Delay")]
    [SerializeField]
    private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField]
    private float minRandomDelay;
    [SerializeField]
    private float maxRandomDelay;

    //pooler 선언
    private objectPooler _pooler;
    private waypoint _waypoint;

    private float _spawnTimer;
    private float _enemiesSpawned;
    private int _enemiesRemaining;

    void Start()
    {
        _pooler = GetComponent<objectPooler>();
        _waypoint = GetComponent<waypoint>();

        _enemiesRemaining = enemyCount; //10
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
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
        //Instantiate(testGO, transform.position, Quaternion.identity);

        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.waypoint = _waypoint;

        enemy.ResetEnemy();
        enemy.transform.localPosition = transform.position;


        newInstance.active = true;
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if(spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwSpawns);
        _enemiesRemaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRemaining--;
        if(_enemiesRemaining <= 0)
        {
            //새 wave 시작
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
