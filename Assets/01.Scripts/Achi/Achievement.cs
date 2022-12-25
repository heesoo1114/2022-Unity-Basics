using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Achievement")]

public class Achievement : ScriptableObject
{
    public string ID; //�������� ID
    public string Title; //�������� �̸�
    public int ProgressToUnlock; // 0/50 ���� �޼�����
    public int GoalReward; //����
    public Sprite Sprite; //��������Ʈ

    private int CurrentProgress;
    public bool IsUnlocked { get; set; }

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

    public string GetProgressCompleted()
    {
        return $"{ProgressToUnlock} / {ProgressToUnlock}";
    }

    private void CheckUnlockStatus()
    {
        if(CurrentProgress >= ProgressToUnlock)
        {
            //�������� �޼�
            UnlockAchievement();
        }
    }

    private void UnlockAchievement()
    {
        IsUnlocked = true;
        AchiManager.OnAchiUnlocked?.Invoke(this);
    }

    private void OnEnable() 
    {
        IsUnlocked = false;
        CurrentProgress = 0;
    }
}
