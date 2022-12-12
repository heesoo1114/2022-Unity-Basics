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

    private void OnEnable()//���� ������Ʈ�� Ȱ��ȭ �ɶ�����
    {
        Enemy.OnEndReached += ReduceLives;
        spawner.OnWaveComplete += WaveComplete;
    }

    private void OnDisable()//���� ������Ʈ�� ��Ȱ��ȭ �ɶ�
    {
        Enemy.OnEndReached -= ReduceLives;
        spawner.OnWaveComplete -= WaveComplete;
    }

    private void WaveComplete()
    {
        CurrentWave++;
    }
}



