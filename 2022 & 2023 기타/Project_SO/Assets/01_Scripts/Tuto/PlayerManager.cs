using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public DataContainer playerSO;

    private void Start() // start : 태어나면 (무조건 한 번만 실행)
    {
        playerSO.SetPlayerName("Yoon");

        Debug.Log($"플레이어 이름 : {playerSO.PlayerName}");
    }
}
