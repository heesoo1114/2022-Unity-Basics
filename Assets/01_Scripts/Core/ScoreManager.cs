using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent LevelUpEvent;

    private float score = 0;
    public float plusAmount = 2;

    private float tempScore = 0;
    public float levelUpAmount = 1000;

    public void Init()
    {
        StopAllCoroutines();
        score = 0;
        tempScore = 0;
        plusAmount = 2;
    }

    public void UpdateGameOverUI()
    {
        UIManager.Instance.GameOverScore(score);
        UIManager.Instance.UpdateBestScore(score);
    }

    public void ScoreCheckStart()
    {
        Init();
        StartCoroutine(CheckScore());
    }

    IEnumerator CheckScore()
    {
        while (true)
        {
            score += plusAmount * Time.deltaTime;

            tempScore += plusAmount * Time.deltaTime;
            if (tempScore >= levelUpAmount)
            {
                LevelUpEvent?.Invoke();
                plusAmount++;
                tempScore = 0;
            }

            UIManager.Instance.UpdateScore(score);
            yield return null;
        }
    }
}
