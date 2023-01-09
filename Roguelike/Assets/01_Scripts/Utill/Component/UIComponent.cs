using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UIComponent : IComponent
{
    private List<UIScreen> uIScreens = new ();

    public UIComponent()
    {
        uIScreens.Add(GameObject.Find("Init Screen").GetComponent<UIScreen>());
        uIScreens.Add(GameObject.Find("Standby Screen").GetComponent<UIScreen>());
        uIScreens.Add(GameObject.Find("Running Screen").GetComponent<UIScreen>());
        uIScreens.Add(GameObject.Find("Result Screen").GetComponent<UIScreen>());
    }

    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                InitAllScreens();
                CloseAllScreen();
                break;

            default:
                ActiveScreen(state);
                break;
        }
    }

    private void ActiveScreen(GameState state)
    {
        CloseAllScreen();

        foreach (var screen in uIScreens.Where(screen => screen.state == state))
        {
            screen.UpdateScreenState(true);
        }
    }

    private void CloseAllScreen()
    {
        foreach (var screen in uIScreens)
        {
            screen.UpdateScreenState(false);
        }
    }

    private void InitAllScreens()
    {
        foreach (var screen in uIScreens)
        {
            screen.Init();
        }
    }
}
