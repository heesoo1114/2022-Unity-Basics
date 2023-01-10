using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

public class PlayerPhysicsComponent : IPlayerComponent
{
    private Subject<float> hpStream = new ();

    private float maxHp = 10;

    private float hp = 10;

    private float speed = 0.5f;

    public PlayerPhysicsComponent(GameObject player) : base(player)
    {

    }

    public override void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                Init();
                break;

            case GameState.STANDBY:
                hp = maxHp;
                hpStream.OnNext(hp);
                break;
        }
    }

    private void Init()
    {
        GameManager.Instance.GetGameComponent<PlayerComponent>().PlayerMoveSubscribe(PlayerPositionEvent);

        player.OnCollisionEnter2DAsObservable().Subscribe(col =>
        {
            if (!col.collider.tag.Equals("Enemy")) return;

            hp--;

            hpStream.OnNext(hp / maxHp);

            if (hp <= 0)
            {
                GameManager.Instance.UpdateState(GameState.RESULT);
            }
        }).AddTo(GameManager.Instance);
    }

    private void PlayerPositionEvent(Vector3 playerPosition)
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction *= Time.deltaTime * speed;

        UpdateTranslate(direction);
    }

    private void UpdateTranslate(Vector2 direction)
    {
        player.transform.Translate(direction);
    }

    public void HpSubscribe(Action<float> action)
    {
        hpStream.Subscribe(action);
    }
}
