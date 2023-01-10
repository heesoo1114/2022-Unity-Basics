using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerComponent : IComponent
{
    private GameObject player;

    private IObservable<Vector3> playerMoveStream;

    private IObservable<Vector3Int> playerChunkMoveStream;

    private const float playerChunkMoveSize = ChunkComponent.ChunkSize * 0.16f;

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

        playerMoveStream = Observable.EveryUpdate()
            //.Where(condition => GameManager.Instance.State == GameState.RUNNING)
            .Select(stream => player.transform.position);

        playerChunkMoveStream = playerMoveStream.Select(position =>
        {
            var pos = position / playerChunkMoveSize;

            pos.x += position.x > 0 ? 0.5f : -0.5f;
            pos.y += position.y > 0 ? 0.5f : -0.5f;

            return new Vector3Int((int)pos.x, (int)pos.y);
        }).DistinctUntilChanged();
           

        components.Add(new PlayerWeaponComponent(player));
        components.Add(new PlayerAnimationComponent(player));
        components.Add(new PlayerPhysicsComponent(player));
        components.Add(new PlayerUIComponent(player));
    }

    public void PlayerMoveSubscribe(Action<Vector3> action) 
    { 
        playerMoveStream.Subscribe(action).AddTo(GameManager.Instance); 
    }

    public T GetPlayerComponent<T>() where T : IPlayerComponent
    {
        var value = default(T);

        foreach (var component in components.OfType<T>())
        {
            value = component;
        }

        return value;
    }

    public void PlayerChunkMoveSubscribe(Action<Vector3Int> action)
    {
        playerChunkMoveStream.Subscribe(action);
    }
}
