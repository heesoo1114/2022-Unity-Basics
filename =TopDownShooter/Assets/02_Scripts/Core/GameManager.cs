using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private Transform _player;
    public Transform Player {  get => _player; }

    [SerializeField]
    private PoolingListSO _poolingList = null;
    [SerializeField]
    private Texture2D _cursorTexture = null;

    [SerializeField]
    private float _criticalRate = 0.7f, _criticalMinDmg = 1.5f, _criticalMaxDmg = 2.5f;

    public bool IsCritical => Random.value < _criticalRate; // Random.value는 0~1까지의 랜덤한 값 뱉는다.
    public int GetCriticalDamage(int dmg)
    {
        float ratio = Random.Range(_criticalMinDmg, _criticalMaxDmg);
        dmg = Mathf.CeilToInt((float)dmg * ratio);
        return dmg;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Multiple Gamemanager is running");
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform);
        CreatePool();
        SetCursorIcon(); 
    }

    private void CreatePool()
    {
        foreach(PoolingPair pp in _poolingList.list)
        {
            PoolManager.Instance.CreatePool(pp.prefab, pp.poolCount);
        }
    }

    private void SetCursorIcon()
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(_cursorTexture.width / 2f, _cursorTexture.height / 2f), CursorMode.Auto);
    }

    private float _nextGenerationTime = 0f;
    private int _spawnCount = 3;
    [SerializeField]
    private float _generateMinTime = 5f, _generateMaxTime = 8f;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_nextGenerationTime);

            float posX = Random.Range(-4.5f, 4.5f);
            float posY = Random.Range(-5f, 5f);
            Spawner spawner = PoolManager.Instance.Pop("Spawner") as Spawner;
            spawner.transform.position = new Vector3(posX, posY);
            spawner.StartToSpawn(_spawnCount);
            _spawnCount++;
            _nextGenerationTime = Random.Range(_generateMinTime, _generateMaxTime);
        }
    }
}
