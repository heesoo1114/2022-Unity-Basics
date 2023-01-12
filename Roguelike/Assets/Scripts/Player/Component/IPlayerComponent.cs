using UnityEngine;

public abstract class IPlayerComponent : IComponent
{

    protected GameObject player;

    public IPlayerComponent(GameObject player)
    {
        this.player = player;
    }

    public abstract void UpdateState(GameState state);
  
}