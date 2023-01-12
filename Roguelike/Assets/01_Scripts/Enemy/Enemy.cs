using System;
using UniRx;
using UnityEngine;

public class Enemy
{

    public Vector3 Position
    {
        set => gameObject.transform.position = value;
        get => gameObject.transform.position;
    }

    protected GameObject gameObject;

    protected Subject<Enemy> enemyDestoryStream = new();

    public Enemy(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public virtual void ReturnObject() { }

    public void DestroySubscribe(Action<Enemy> action)
    {
        enemyDestoryStream.Subscribe(action);
    }

    public static class EnemyBuilder
    {
        public static Enemy Build(PoolObjectType type)
        {
            var gameObject = ObjectPool.Instance.GetObject(type);

            switch (type)
            {
                case PoolObjectType.Slime:
                    return new Slime(gameObject);
                case PoolObjectType.Skeleton:
                    return new Skeleton(gameObject);
                default:
                    return new Enemy(gameObject);
            }
        }
    }

}