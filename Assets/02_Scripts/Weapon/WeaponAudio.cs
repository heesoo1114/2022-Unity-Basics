using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    private AudioClip _shootBulletClip = null;
    private AudioClip _outOfBulletClip = null;
    private AudioClip _reloadClip = null;

    public void SetAudioClip(AudioClip shoot, AudioClip outOf, AudioClip reload)
    {
        _shootBulletClip = shoot;
        _outOfBulletClip = outOf;
        _reloadClip = reload;
    }

    public void PlayShootSound()
    {
        PlayClip(_shootBulletClip);
    }
    public void PlayNoBulletSound()
    {
        PlayClip(_outOfBulletClip);
    }
    public void PlayReloadSound()
    {
        PlayClip(_reloadClip);
    }
}
