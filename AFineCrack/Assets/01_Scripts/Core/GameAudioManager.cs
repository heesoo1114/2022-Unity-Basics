using UnityEngine;

public class GameAudioManager : AudioPlayer
{
    [SerializeField] private AudioClip _dieClip;  // overEvent
    [SerializeField] private AudioClip _dashClip; // dash
    [SerializeField] private AudioClip _windClip; // startEvent

    public void PlayDieSound()
    {
        PlayClip(_dieClip);
    }

    public void PlayDashSound()
    {
        PlayClipwithVariablePitch(_dashClip);
    }

    public void PlayStartSound()
    {
        PlayClip(_windClip);
    }

}
