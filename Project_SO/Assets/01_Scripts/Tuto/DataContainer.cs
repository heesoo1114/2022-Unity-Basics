using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fileName�� ó�� �޴����� ������� �� ������ ���� �̸����� ������
[CreateAssetMenu(fileName = "DB Container", menuName = "Game/Player DB")]
public class DataContainer : ScriptableObject
{
    // ������ �ʵ�
    [SerializeField] private string playerName;
    public string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }
    [SerializeField] private int    playerLV;
    [SerializeField] private float  playerHealth;

    // ������ ���� �Լ�
    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
