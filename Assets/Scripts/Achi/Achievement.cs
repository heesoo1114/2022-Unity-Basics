using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Achievement")]

public class Achievement : ScriptableObject
{
    public string ID; //도전과제 ID
    public string Title; //도전과제 이름
    public int ProgressToUnlock; // 0/50 현재 달성정도
    public int GoalReward; //보상
    public Sprite Sprite; //스프라이트

    private int CurrentProgress;

    public void AddProgress(int amount)
    {
        CurrentProgress += amount;
        AchiManager.OnProgressUpdated?.Invoke(this);
        CheckUnlockStatus();
    }

    public string GetProgress()
    {
        return $"{CurrentProgress} / {ProgressToUnlock}";
    }

    private void CheckUnlockStatus()
    {
        if(CurrentProgress >= ProgressToUnlock)
        {
            //도전과제 달성
            UnlockAchievement();
        }
    }

    private void UnlockAchievement()
    {
        AchiManager.OnAchiUnlocked?.Invoke(this);
    }
}
