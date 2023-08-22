using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Intance = null;
    private SaveSystem saveSystem;

    public UnityEvent<bool> OnResultData;

    private void Awake()
    {
        if (Intance == null) 
        {
            Intance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        OnResultData?.Invoke(File.Exists(Application.dataPath + "/SaveData/ SaveFile.txt"));
    }   
    
    public void ClickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ClickLoad()
    {
        StartCoroutine(LoadRoutine());
    }

    IEnumerator LoadRoutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        while (!operation.isDone)
        {
            yield return null;
        }

        saveSystem = FindObjectOfType<SaveSystem>();
        saveSystem.Load();
    }
}
