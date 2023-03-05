using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : AudioPlayer
{
    public AudioClip bgm;
    AudioSource bgmSource;

    private void Awake()
    {
        bgmSource = GetComponent<AudioSource>();
    }

    public void bgmStop()
    {
        bgmSource.Stop();
    }
}
