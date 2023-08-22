using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementCard : MonoBehaviour
{
    [SerializeField] private Image achievementImage; 
    [SerializeField] private TextMeshProUGUI title; 
    [SerializeField] private TextMeshProUGUI progress; 
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Button rewardButton;

    public Achievement AchiLoaded { get; set; }

    public void SetUpAchi(Achievement achievement)
    {
        AchiLoaded = achievement;

        achievementImage.sprite = achievement.Sprite;
        title.text = achievement.Title;
        progress.text = achievement.GetProgress();
        reward.text = achievement.GoalReward.ToString();
    }

    public void GetReward()
    {
        if (AchiLoaded.IsUnlocked)
        {
            MoneySystem.Instance.AddCoins(AchiLoaded.GoalReward);
            rewardButton.gameObject.SetActive(false);
        }
    }

    private void CheckRewardButtonStatus()
    {
        if (AchiLoaded.IsUnlocked)
            rewardButton.interactable = true;
        else
            rewardButton.interactable = false;
    }

    private void AchiUnlocked(Achievement achi)
    {
        if (AchiLoaded == achi)
        {
            //rewardButton.interactable = true;
            CheckRewardButtonStatus();
        }
    }

    private void LoadAchiProgress()
    {
        if (AchiLoaded.IsUnlocked)
            progress.text = AchiLoaded.GetProgressCompleted();
        else
            progress.text = AchiLoaded.GetProgress();
    }

    private void OnEnable()
    {
        CheckRewardButtonStatus();
        LoadAchiProgress();

        AchiManager.OnProgressUpdated += UpdateProgress;
        AchiManager.OnAchiUnlocked += AchiUnlocked;
    }

    private void OnDisable()
    {
        AchiManager.OnProgressUpdated -= UpdateProgress;
        AchiManager.OnAchiUnlocked -= AchiUnlocked;
    }

    private void UpdateProgress(Achievement achiWithProgress)
    {
        if(AchiLoaded == achiWithProgress)
        {
            LoadAchiProgress();
        }
    }
}
