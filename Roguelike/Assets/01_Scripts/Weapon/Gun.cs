using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using DG.Tweening;

public class Gun : IWeapon
{
    private List<GameObject> bullets = new ();

    private List<GameObject> enemies;

    private GameObject player;

    private float delay = 1f;

    public Gun(GameObject player)
    {
        this.player = player;

        GameManager.Instance.GetComponent<EnemyComponent>().EnemiesSubscribe(enemies => this.enemies = enemies);

        Init();
    }

    private void Init()
    {
        Observable
            .Timer(TimeSpan.FromSeconds(delay))
            .Where(condition => GameManager.Instance.State == GameState.RUNNING)
            .Repeat()
            .Subscribe(time => Fire())
            .AddTo(GameManager.Instance);
    }

    private void Fire()
    {
        if (enemies.Count <= 0) return;

        var bullet = ObjectPool.Instance.GetObject(PoolObjectType.Bullet);

        bullet.transform.position = player.transform.position;

        var normalized = (enemies[0].transform.position - bullet.transform.position).normalized;

        var angle = MathF.Atan2(normalized.x, normalized.y) * Mathf.Rad2Deg - 90;

        var angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.transform.rotation = angleAxis;

        bullet.transform.DOMove(normalized * 100, 50)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, bullet);
            });

        bullets.Add(bullet);
    }

    public void Reset()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].transform.DOKill();

            ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, bullets[i]);
        }
        
        bullets.Clear();
    }
}
