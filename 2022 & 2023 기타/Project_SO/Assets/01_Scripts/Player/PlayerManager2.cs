using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    public PlayerData _playerData;

    private void Start()
    {
        _playerData = Resources.Load<PlayerData>("PlayerData");
        Debug.Log($"�÷��̾� �̸� {_playerData.PlayerName}");

        _playerData.name = "Lee";
    }
}
