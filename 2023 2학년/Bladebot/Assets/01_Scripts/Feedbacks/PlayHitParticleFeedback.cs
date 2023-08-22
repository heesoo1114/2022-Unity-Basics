using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHitParticleFeedback : Feedback
{
    [SerializeField] private PoolAbleMono _hitParticle;
    [SerializeField] private float _effectPlayTime;

    private AIActionData _aiActionData;
    private void Awake()
    {
        _aiActionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_hitParticle.name) as EffectPlayer;
        effect.transform.position = _aiActionData.HitPoint;
        effect.StartPlay(_effectPlayTime);
    }

    public override void FinishFeedback()
    {
        // do nothing
    }
}
