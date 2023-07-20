using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public GameSoundSO settingData;

    private void Start()
    {
        settingData = Resources.Load<GameSoundSO>("GameSettingData");

        Debug.Log("���� ��� ���� : " + settingData.SoundEnabled);
        Debug.Log("���� ���� : " + settingData.MusicVolume);
        Debug.Log("ȿ���� ���� : " + settingData.SfxVolume);
    }
}
