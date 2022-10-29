using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform _playerTrm;
    public Transform PlayerTrm => _playerTrm;

    public static GameManager Instance;

    private void Awake()
    {
        _playerTrm = GameObject.Find("Player").transform;

        if (Instance != null)
        {
            Debug.LogError("Multiple Gamemanager is running!");
        }
        Instance = this;
    }
}
