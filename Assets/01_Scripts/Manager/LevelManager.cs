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

    private void OnEnable() // ���� ������Ʈ�� Ȱ��ȭ �� ������
    {
        Enemy.OnEndReached += ReduceLives;
    }

    private void OnDisable() // ���� ������Ʈ�� ��Ȱ��ȭ �� ��
    {
        Enemy.OnEndReached -= ReduceLives;   
    }
}
