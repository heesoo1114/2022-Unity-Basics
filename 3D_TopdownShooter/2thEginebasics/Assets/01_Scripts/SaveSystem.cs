using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public Vector2 PlayerPos;
}

public class SaveSystem : MonoBehaviour
{
    private SaveData saveData;
    private string savePath;
    private string saveFileName = "/SaveFile.txt";
    private PlayerMove player;

    private void Start()
    {
        saveData = new SaveData();
        savePath = Application.dataPath + "/SaveData/";

        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
    }

    [ContextMenu("저장")]
    public void Save()
    {
        player = FindObjectOfType<PlayerMove>();
        saveData.PlayerPos = player.transform.position;
        
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath + saveFileName, json);

        Debug.Log("저장완료");
        Debug.Log(json);
    }

    [ContextMenu("불러오기")]
    public void Load()
    {
        if (File.Exists(savePath + saveFileName))
        {
            string loadJson = File.ReadAllText(savePath + saveFileName);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            player = FindObjectOfType<PlayerMove>();
            player.transform.position = saveData.PlayerPos;

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("저장 파일 없음");
        }
    }
}
