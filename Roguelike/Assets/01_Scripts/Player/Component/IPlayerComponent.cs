using UnityEngine;

public abstract class IPlayerComponent : IComponent
{
    protected GameObject player;

    protected PlayerData playerData;

    public IPlayerComponent(GameObject player)
    {
        this.player = player;
    }

    public IPlayerComponent(GameObject player, PlayerData playerData)
    {
        this.player = player;
        this.playerData = playerData;
    }

    public abstract void UpdateState(GameState state);
  
}