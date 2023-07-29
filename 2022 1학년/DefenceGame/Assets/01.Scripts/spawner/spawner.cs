using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SpawnModes
{
    Fixed,
    Random
}
public class spawner : MonoBehaviour
{
    public static Action OnWaveComplete;

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

    [Header("Poolers")]
    [SerializeField] private List<objectPooler> enemyWavePooler;

    //pooler 선언
    private waypoint _waypoint;

    private float _spawnTimer;
    private float _enemiesSpawned;
    private int _enemiesRemaining;

    void Start()
    {
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
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        //Instantiate(testGO, transform.position, Quaternion.identity);

        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.waypoint = _waypoint;

        enemy.ResetEnemy();
        enemy.transform.localPosition = _waypoint.GetWaypointPosition(0);


        newInstance.active = true;
    }

    private objectPooler GetPooler()
    {
        int CurrentWave = LevelManager.Instance.CurrentWave;
        if (CurrentWave % 50 <= 10)
            return enemyWavePooler[0];
        else if (CurrentWave % 40 <= 10)
            return enemyWavePooler[1];
        else if (CurrentWave % 30 <= 10)
            return enemyWavePooler[2];
        else if (CurrentWave % 20 <= 10)
            return enemyWavePooler[3];
        else
            return enemyWavePooler[4];
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
        float randomTimer = UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
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
            OnWaveComplete?.Invoke();

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
