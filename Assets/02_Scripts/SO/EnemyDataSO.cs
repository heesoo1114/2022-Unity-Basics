using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemies/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int maxHealth = 10;
    public float knockbackRegist = 1f;

    public int damage = 1;
    public float attackDelay = 1;
    public float attackRange = 0.8f;
    public float chaseRange = 5f;
}
