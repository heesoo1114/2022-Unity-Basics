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

    public Achievement AchiLoaded { get; set; }

    public void SetUpAchi(Achievement achievement)
    {
        AchiLoaded = achievement;

        achievementImage.sprite = achievement.Sprite;
        title.text = achievement.Title;
        progress.text = achievement.GetProgress();
        reward.text = achievement.GoalReward.ToString();
    }

    private void OnEnable()
    {
        AchiManager.OnProgressUpdated += UpdateProgress;
    }

    private void OnDisable()
    {
        AchiManager.OnProgressUpdated -= UpdateProgress;
    }

    private void UpdateProgress(Achievement achiWithProgress)
    {
        if(AchiLoaded == achiWithProgress)
        {
            progress.text = achiWithProgress.GetProgress();
        }
    }
}
