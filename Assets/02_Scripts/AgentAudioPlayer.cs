using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudioPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip _stepClip;

    public void PlayStepSound()
    {
        PlayClipwithVariablePitch(_stepClip);
    }
}
