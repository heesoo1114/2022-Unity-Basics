using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _firePos;
    [SerializeField] private float _power = 20f;

    public void Attack()
    {
        var bullet = Instantiate(_bullet, _firePos.position, Quaternion.identity);
        bullet.Fire(_firePos.forward * _power);
    }
}
