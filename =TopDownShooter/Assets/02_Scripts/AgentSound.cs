using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    [SerializeField]
    private AudioClip _hitClip = null, _deathClip = null, _attackClip = null;

    public void PlayHitSound()
    {
        PlayClipwithVariablePitch(_hitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(_deathClip);
    }

    public void PlayAttackSound()
    {
        PlayClip(_attackClip);
    }
}
