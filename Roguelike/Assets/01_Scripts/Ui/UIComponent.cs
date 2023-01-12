using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIComponent : IComponent
{
    private List<UIScreen> screens = new();

    public UIComponent()
    {
        screens.Add(GameObject.Find("Init Screen").GetComponent<UIScreen>());
        screens.Add(GameObject.Find("Standby Screen").GetComponent<UIScreen>());
        screens.Add(GameObject.Find("Running Screen").GetComponent<UIScreen>());
        screens.Add(GameObject.Find("GameOverScreen").GetComponent<UIScreen>());
        screens.Add(GameObject.Find("StageCompleteScreen").GetComponent<UIScreen>());
    }
    
    public void UpdateState(GameState state)
    {
        Debug.Log(state);
        
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

        foreach (var screen in screens.Where(screen => screen.screenState == state))
            screen.UpdateScreenState(true);
    }

    private void CloseAllScreen()
    {
        foreach (var screen in screens)
        {
            screen.UpdateScreenState(false);
        }
    }

    private void InitAllScreens()
    {
        foreach (var screen in screens)
        {
            screen.Init();
        }
    }
    
}