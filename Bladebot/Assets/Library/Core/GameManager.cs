using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;

    private Transform _playerTrm;
    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
            {
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _playerTrm;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        CreatePool();
        CreateTimeContoller();
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _poolingList.Pairs.ForEach(pair =>
        {
            PoolManager.Instance.CreatePool(pair.Prefab, pair.Count);
        });
    }
    private void CreateTimeContoller()
    {
        TimeController.Instance = gameObject.AddComponent<TimeController>();
    }
}
