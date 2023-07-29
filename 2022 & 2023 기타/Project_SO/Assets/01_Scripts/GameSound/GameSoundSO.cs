using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingData")]
public class GameSoundSO : ScriptableObject
{
    private bool soundEnabled;
    private float musicVolume;
    private float sfxVolume;

    public bool SoundEnabled
    {
        get => soundEnabled;
        set => soundEnabled = value;
    }

    public float MusicVolume
    {
        get => musicVolume;
        set => musicVolume = value;
    }

    public float SfxVolume
    {
        get => sfxVolume;
        set => sfxVolume = value;
    }
}
