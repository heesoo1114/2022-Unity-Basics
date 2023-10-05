using UnityEngine.Playables;
using UnityEngine;

public class SlowMotion : PlayableBehaviour
{
    public float timeValue = 0.3f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Time.timeScale = timeValue;
    }
}
