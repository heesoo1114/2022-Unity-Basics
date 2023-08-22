using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    private enum State
    {
        Waiting,
        SpawnWave,
    }

    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private Transform nextSpawnWavePosition;

    public event EventHandler OnWaveNumberChanged;

    private State state;
    private int waveNumber;

    private float waveSpawnTimer;
    private float enemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        state = State.Waiting;
        spawnPosition= spawnPositionList[Random.Range(0, spawnPositionList.Count)].position;
        nextSpawnWavePosition.position = spawnPosition;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Waiting:
                waveSpawnTimer -= Time.deltaTime;
                if (waveSpawnTimer < 0f)
                {
                    SpawnWave();
                    state = State.SpawnWave;
                    waveNumber++;
                    OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.SpawnWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    enemySpawnTimer -= Time.deltaTime;
                    if (enemySpawnTimer < 0f)
                    {
                        enemySpawnTimer = Random.Range(0f, .2f);
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;
                        if (remainingEnemySpawnAmount <= 0)
                        {
                            state = State.Waiting;
                            spawnPosition = spawnPositionList[Random.Range(0, spawnPositionList.Count)].position;
                            nextSpawnWavePosition.position = spawnPosition;
                            waveSpawnTimer = 10f;
                        }
                    }
                }
                break;
        }
     
    }
    private void SpawnWave()
    {
        remainingEnemySpawnAmount = 5+waveNumber*3;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        return waveSpawnTimer;
    }


}
