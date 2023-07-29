using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fileName은 처음 메뉴에서 만들었을 때 설정된 값이 이름으로 설정됨
[CreateAssetMenu(fileName = "DB Container", menuName = "Game/Player DB")]
public class DataContainer : ScriptableObject
{
    // 데이터 필드
    [SerializeField] private string playerName;
    public string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }
    [SerializeField] private int    playerLV;
    [SerializeField] private float  playerHealth;

    // 데이터 조작 함수
    public void SetPlayerName(string name)
    {
        playerName = name;
    }
}
