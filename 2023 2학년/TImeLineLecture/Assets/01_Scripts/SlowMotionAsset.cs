using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SlowMotionAsset : PlayableAsset
{
    public float timeValue = 1f;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SlowMotion>.Create(graph);
        var behaviour = playable.GetBehaviour();

        behaviour.timeValue = timeValue;
        return playable;
    }
}
