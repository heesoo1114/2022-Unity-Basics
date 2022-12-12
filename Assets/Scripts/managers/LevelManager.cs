using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    public int TotalLives {get; set; }
    public int CurrentWave { get; set; }

    private void Start()
    {
        TotalLives = lives;
        if(TotalLives <= 0)
        {
            TotalLives = 0;
        }
        CurrentWave = 1;

    }
    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
    }

    private void OnEnable()//게임 오브젝트가 활성화 될때마다
    {
        Enemy.OnEndReached += ReduceLives;
        spawner.OnWaveComplete += WaveComplete;
    }

    private void OnDisable()//게임 오브젝트가 비활성화 될때
    {
        Enemy.OnEndReached -= ReduceLives;
        spawner.OnWaveComplete -= WaveComplete;
    }

    private void WaveComplete()
    {
        CurrentWave++;
    }
}



