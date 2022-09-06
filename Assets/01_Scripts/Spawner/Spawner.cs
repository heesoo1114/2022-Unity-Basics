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
    [SerializeField]
    private float fixedTimer; // = delayBtnSpawn

    [Header("Random Delay")]
    [SerializeField]
    private float minRandomDelay;
    [SerializeField]
    private float maxRandomDelay;

    // 풀러 선언
    private ObjPooler _pooler;

    private float _spawnTimer;
    private float _enemiesSpawned;

    private void Start()
    {
        _pooler = GetComponent<ObjPooler>();
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
        // Instantiate(testGO, transform.position, Quaternion.identity);
        newInstance.SetActive(true);
        print("spawn");
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

}
