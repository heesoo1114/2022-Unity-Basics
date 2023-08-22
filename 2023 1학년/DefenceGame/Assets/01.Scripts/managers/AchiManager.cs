using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchiManager : Singleton<AchiManager>
{
    public static Action<Achievement> OnAchiUnlocked;
    public static Action<Achievement> OnProgressUpdated;

    [SerializeField] private AchievementCard achiCardPrefab;
    [SerializeField] private Transform achiPanelContainer;
    [SerializeField] private Achievement[] achis;
    void Start()
    {
        LoadAchis();   
    }

    private void LoadAchis()
    {
        for(int i = 0; i < achis.Length; i++)
        {
            AchievementCard card = Instantiate(achiCardPrefab, achiPanelContainer);
            card.SetUpAchi(achis[i]);
        }
    }

    public void AddProgress(string achiID, int amount)
    {
        Achievement achiWanted = AchiExists(achiID);
        if (achiWanted != null)
        {
            achiWanted.AddProgress(amount);
        }
    }

    private Achievement AchiExists(string achiID)
    {
        for(int i = 0; i < achis.Length; i++)
        {
            if(achis[i].ID == achiID)
            {
                return achis[i];
            }
        }
        return null;
    }
}
