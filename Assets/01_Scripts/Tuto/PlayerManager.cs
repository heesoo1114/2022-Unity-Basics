using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public DataContainer playerSO;

    private void Start() // start : �¾�� (������ �� ���� ����)
    {
        playerSO.SetPlayerName("Yoon");

        Debug.Log($"�÷��̾� �̸� : {playerSO.PlayerName}");
    }
}
