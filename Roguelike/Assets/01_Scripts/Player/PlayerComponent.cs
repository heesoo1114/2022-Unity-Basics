using UnityEngine;
using UniRx;
using System;

public class PlayerComponent : IComponent
{
    private GameObject player;

    private IObservable<Vector3> playerMoveStream;

    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                Init();
                break;
        }
    }

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerMoveStream = Observable.EveryUpdate().Select(stream => player.transform.position);
    }

    public void PlayerMoveSubscribe(Action<Vector3> action) 
    { 
        playerMoveStream.Subscribe(action).AddTo(GameManager.Instance); 
    }
}
