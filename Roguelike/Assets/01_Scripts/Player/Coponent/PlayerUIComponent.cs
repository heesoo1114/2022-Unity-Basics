using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : IPlayerComponent
{
    private Image hp;

    public PlayerUIComponent(GameObject player) : base(player)
    {
        hp = player.transform.GetChild(0).GetChild(0).GetComponent<Image>();
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
        GameManager.Instance.GetGameComponent<PlayerComponent>().
            GetPlayerComponent<PlayerPhysicsComponent>().
            HpSubscribe(HpUpdateEvent);
    }

    private void HpUpdateEvent(float hp)
    {
        this.hp.fillAmount = hp;
    }
}
