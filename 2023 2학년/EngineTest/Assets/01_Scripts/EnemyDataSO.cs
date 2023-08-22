using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyDataSo")]
public class EnemyDataSO : ScriptableObject
{
    public int MaxHP;
    public float MoveSpeed;
    public float RotateSpeed;
    public int AtkDamage;
    public float MotionDelay; 
    public float AtkCooltime;
}
