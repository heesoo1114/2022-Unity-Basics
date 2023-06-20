using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private PoolingListSO _poolingList = null;

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

    private void CreatePool()
    {
        foreach (PoolingPair pp in _poolingList.list)
        {
            PoolManager.Instance.CreatePool(pp.prefab, pp.poolCount);
        }
    }
}
