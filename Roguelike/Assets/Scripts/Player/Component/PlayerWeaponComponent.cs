using UnityEngine;

public class PlayerWeaponComponent : IPlayerComponent
{

    private IWeapon weapon;

    public PlayerWeaponComponent(GameObject player) : base(player)
    {

    }

    public override void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                Init();

                break;
            case GameState.RESULT:
                weapon.Reset();

                break;
        }
    }

    private void Init()
    {
        weapon = new Gun(player);
    }

}