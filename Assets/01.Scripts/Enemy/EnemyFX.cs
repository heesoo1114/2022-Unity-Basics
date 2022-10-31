using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPos;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void EnemyHit(Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;

            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPos);
            newInstance.transform.position = textDamageSpawnPos.position;
            newInstance.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Projectile.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        Projectile.OnEnemyHit -= EnemyHit;
    }
}
