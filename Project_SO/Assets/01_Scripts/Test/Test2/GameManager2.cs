/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    private static GameManager2 instance;
    private static GameManager2 Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager2>();
            return instance;
        }
    }

    private void Start()
    {
        GameSettingSO setting = Resources.Load<GameSettingSO>("GameSettingSO");
    }

    private void GameSetting(GameSettingSO gameSettingSo)
    {

    }

    // ����� ����
    private float masterVolum = 1f;
    private float musicVolum = 1f;
    private float sfxVolum = 1f;

    public static void SetMasterVolume(float volume)
    {
        Instance.masterVolum = Mathf.Clamp01(volume);
        //����� ���� ���� 
    }

    public static void SetMusicVolume(float volume)
    {
        Instance.musicVolum = Mathf.Clamp01(volume);
        //����� ���� ���� 
    }

    public static void SetSfxVolume(float volume)
    {
        Instance.sfxVolum = Mathf.Clamp01(volume);
        //����� ���� ���� 
    }
}
*/