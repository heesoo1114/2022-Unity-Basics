using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

// Random이 모호한 참조라 확실히 정해줌
using Random = UnityEngine.Random;

public class EnemyComponent : MonoBehaviour, IComponent
{
    [SerializeField] private GameObject enemyPrefab;

    private List<Enemy> enemies = new ();

    // subject: stream을 수동적으로 가져올 수 있게
    private Subject<List<Enemy>> enemiesStream = new ();

    private int enemyCount = 10;

    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                Init();
                break;

            case GameState.STANDBY:
                Reset();
                break;

            case GameState.RUNNING:
                Generate();
                break;
        }
    }

    private void Init()
    {
        GameManager.Instance.GetGameComponent<PlayerComponent>().PlayerMoveSubscribe(PlayerMoveEvent);                  
    }

    private void PlayerMoveEvent(Vector3 playerPosition)
    {
        foreach (var enemy in enemies)
        {
            var movePosition = GetPosition(enemy.Position, playerPosition);

            enemy.Position = movePosition;
        }
    }

    private Vector3 GetPosition(Vector3 enemyPosition, Vector3 playerPosition)
    {
        var normal = (playerPosition - enemyPosition).normalized;

        enemyPosition += normal * Time.deltaTime * .25f;

        return enemyPosition;
    }

    private void Generate()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            enemies.Add(Enemy.EnemyBuilder.Build(PoolObjectType.Slime));

            enemies[^1].Position = GetRandomPosition();

            enemies[^1].DestorySubscribe(EnemyDestoryEvent);
        }

        enemiesStream.OnNext(enemies);
    }

    private void EnemyDestoryEvent(Enemy target)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].Equals(target))
            {
                enemies.RemoveAt(i);
                return;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        var angle = Random.Range(0, 361) * Mathf.Rad2Deg;

        var position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 2;

        return position;
    }

    private void Reset()
    {
        for (var i = 0; i < enemies.Count; i++)
        {
            enemies[i].ReturnObject();
        }

        enemies.Clear();
    }

    public void EnemiesSubscribe(Action<List<Enemy>> action)
    {
        enemiesStream.Subscribe(action);
    }
}
