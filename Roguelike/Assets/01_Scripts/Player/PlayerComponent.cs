using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

public class PlayerComponent : IComponent
{
    private GameObject player;

    private IObservable<Vector3> playerMoveStream;

    private List<IPlayerComponent> components = new ();

    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                Init();
                break;

            case GameState.STANDBY:
                player.transform.position = Vector3.zero;
                break;
        }

        foreach (var component in components)
        {
            component.UpdateState(state);
        }
    }

    private void Init()
    {
        player = ObjectPool.Instance.GetObject(PoolObjectType.Player);

        playerMoveStream = Observable.EveryUpdate().Select(stream => player.transform.position);

        components.Add(new PlayerWeaponComponent(player));
    }

    public void PlayerMoveSubscribe(Action<Vector3> action) 
    { 
        playerMoveStream.Subscribe(action).AddTo(GameManager.Instance); 
    }
}
