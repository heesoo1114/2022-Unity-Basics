using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public UnityEvent GameStartEvent;
    public UnityEvent GameOverEvent;

    [SerializeField]
    private PoolingListSO _poolingList = null;

    private bool isPlaying = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple Gamemanager is running");
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform);
        CreatePool();
    }

    private void Update()
    {
        if (isPlaying == false && (Input.GetMouseButtonDown(0)))
        {
            Debug.Log("game start input done");
            GameStart();
        }
    }

    private void CreatePool()
    {
        foreach (PoolingPair pp in _poolingList.list)
        {
            PoolManager.Instance.CreatePool(pp.prefab, pp.poolCount);
        }
    }

    public void GameStart()
    {
        GameStartEvent?.Invoke();
        isPlaying = true;
        print("Game Start");
    }

    public void GameOver()
    {
        GameOverEvent?.Invoke();
        isPlaying = false;
        print("Game Over");
    }
}
