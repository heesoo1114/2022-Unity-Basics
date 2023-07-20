using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public GameSoundSO settingData;

    private void Start()
    {
        settingData = Resources.Load<GameSoundSO>("GameSettingData");

        Debug.Log("사운드 사용 여부 : " + settingData.SoundEnabled);
        Debug.Log("음악 볼륨 : " + settingData.MusicVolume);
        Debug.Log("효과음 볼륨 : " + settingData.SfxVolume);
    }
}
