using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPos;
    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyHit(Enemy enemy, float damage)
    {
        if(_enemy == enemy) //텍스트 애니메이션 이펙트
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
