using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform _playerTrm;
    public Transform PlayerTrm => _playerTrm;

    public static GameManager Instance;

    [SerializeField]
    private Transform _mapTrm;

    private void Awake()
    {
        _playerTrm = GameObject.Find("Player").transform;

        if (Instance != null)
        {
            Debug.LogError("Multiple Gamemanager is running!");
        }
        Instance = this;

        MapManager.Instance = new MapManager(_mapTrm);
    }
}
