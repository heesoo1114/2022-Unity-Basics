using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : PoolAbleMono
{
    [SerializeField]
    protected List<ParticleSystem> _particles;

    [SerializeField]
    protected List<VisualEffect> _effects;

    public void StartPlay(float endTime)
    {
        if (_particles != null)
        {
            _particles.ForEach(p => p.Play());
        }
        if (_effects != null)
        {
            _effects.ForEach(e => e.Play());
        }

        StartCoroutine(Timer(endTime));
    }

    private IEnumerator Timer(float endTime)
    {
        yield return new WaitForSeconds(endTime);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        if (_particles != null)
        {
            _particles.ForEach(p => p.Simulate(0));
        }
        if (_effects != null)
        {
            _effects.ForEach(e => e.Stop());
        }
    }
}
