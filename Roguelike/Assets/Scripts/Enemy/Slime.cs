using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Slime : Enemy
{

    private IDisposable disposable;

    public Slime(GameObject gameObject) : base(gameObject)
    {
        disposable = gameObject
            .OnTriggerEnter2DAsObservable()
            .Subscribe(OnTrigger2DEnterEvent);
    }

    private void OnTrigger2DEnterEvent(Collider2D collider2D)
    {
        if (!collider2D.tag.Equals("Bullet"))
            return;

        ReturnObject();

        enemyDestoryStream.OnNext(this);
    }

    public override void ReturnObject()
    {
        ObjectPool.Instance.ReturnObject(PoolObjectType.Slime, gameObject);

        disposable.Dispose();
    }
}