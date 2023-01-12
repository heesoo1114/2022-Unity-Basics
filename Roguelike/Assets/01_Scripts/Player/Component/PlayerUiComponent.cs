using UnityEngine;
using UnityEngine.UI;

public class PlayerUiComponent : IPlayerComponent
{

    private Image hp;

    public PlayerUiComponent(GameObject player) : base(player)
    {
        hp = player.transform.GetChild(0).GetChild(0)
            .GetComponent<Image>();
    }

    public override void UpdateState(GameState state)
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
        GameManager.Instance.GetGameComponent<PlayerComponent>()
            .GetPlayerComponent<PlayerPhysicsComponent>()
            .PlayerDataSubscribe(PlayerDataEvent);
    }

    private void PlayerDataEvent(PlayerData playerData)
    {
        this.hp.fillAmount = playerData.hp / playerData.maxhp;
    }
}