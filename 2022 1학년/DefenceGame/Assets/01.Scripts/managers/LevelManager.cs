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
        CurrentWave = 1;

    }
    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if(TotalLives <= 0)
        {
            TotalLives = 0;
            UIManager.Instance.ShowGameOverPanel();
        }
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

        AchiManager.Instance.AddProgress("Wave10", 1);
        AchiManager.Instance.AddProgress("Wave20", 1);
        AchiManager.Instance.AddProgress("Wave50", 1);
        AchiManager.Instance.AddProgress("Wave100", 1);
    }
}



