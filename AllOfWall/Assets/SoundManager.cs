using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : AudioPlayer
{
    public AudioClip gameOverSound;
    public AudioClip successSound;

    public void GameOver()
    {
        PlayClip(gameOverSound);
    }

    public void Success()
    {
        PlayClip(successSound);
    }
}
