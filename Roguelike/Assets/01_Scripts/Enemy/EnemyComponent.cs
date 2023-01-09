using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour, IComponent
{
    [SerializeField] private GameObject enemyPrefab;

    private List<GameObject> enemies = new ();

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
        for (int i = 0; i < enemies.Count; i++)
        {
            var movePosition = GetPosition(enemies[i].transform.position, playerPosition);

            enemies[i].transform.position = movePosition;
        }
    }

    private Vector3 GetPosition(Vector3 enemyPosition, Vector3 playerPosition)
    {
        var normal = (playerPosition - enemyPosition).normalized;

        enemyPosition += normal * Time.deltaTime;

        return enemyPosition;
    }

    private void Generate()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var enemy = ObjectPool.Instance.GetObject(PoolObjectType.Enemy);

            enemy.transform.position = GetRandomPosition();

            enemies.Add(enemy);
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
            ObjectPool.Instance.ReturnObject(PoolObjectType.Enemy, enemies[i]);
        }

        enemies.Clear();
    }
}
