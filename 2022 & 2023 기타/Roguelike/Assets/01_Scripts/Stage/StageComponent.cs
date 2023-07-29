using System;
using System.IO;
using UniRx;
using UnityEngine;

public class StageComponent : IComponent
{

    private Subject<Stage> stageStream = new ();

    private Stage stage;

    public void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.INIT:
                break;

            case GameState.RUNNING:
                var paths = BetterStreamingAssets.GetFiles("/Data", "NormalStage.json", SearchOption.AllDirectories);
                stage = JsonUtility.FromJson<Stage>(BetterStreamingAssets.ReadAllText(paths[0]));
                stageStream.OnNext(stage);
                break;
        }
    }

    public void StageSubscribe(Action<Stage> action)
    {
        stageStream.Subscribe(action);
    }

}