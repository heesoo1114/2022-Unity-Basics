using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : UIScreen
{
    [SerializeField] private Button cancelButton;

    [SerializeField] private Button[] itemButtons;

    private Subject<int> selectItemStream = new ();

    public override void UpdateScreenState(bool open)
    {

    }

    public override void Init()
    {
        GameManager.Instance.GetGameComponent<PlayerComponent>()
            .GetPlayerComponent<PlayerPhysicsComponent>()
            .PlayerLevelUpSubscibe(level =>
            {
                if (level == 0)
                {
                    return;
                }

                base.UpdateScreenState(true);

                Time.timeScale = 0;
            });

        cancelButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;


        });
    }

    public void SelectItemSubscribe(Action<int> action)
    {
        selectItemStream.Subscribe(action);
    }
}
