using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float lives = 10;

    private void ReduceLives()
    {
        lives--;
    }

    private void OnEnable() // 게임 오브젝트가 활성화 될 때마다
    {
        Enemy.OnEndReached += ReduceLives;
    }

    private void OnDisable() // 게임 오브젝트가 비활성화 될 때
    {
        Enemy.OnEndReached -= ReduceLives;   
    }
}
