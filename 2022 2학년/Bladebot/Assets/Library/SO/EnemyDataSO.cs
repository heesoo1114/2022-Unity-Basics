using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/EnemyDataSo")]
public class EnemyDataSO : ScriptableObject
{
    public int   MaxHP;
    public float MoveSpeed;
    public float RotateSpeed;
    public int   AtkDamage;
    public float MotionDelay; // 공격 딜레이
    public float AtkCooltime; // 공격 쿨타임
}
