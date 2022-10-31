using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : Singleton<MoneySystem>
{
    [SerializeField] private int coinTest;
    public int TotalCoins { get; set; }
    private string MONEY_SAVE_KEY = "MYGAME_MONEY";

    private void Start()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(MONEY_SAVE_KEY, coinTest);
    }

    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(MONEY_SAVE_KEY, TotalCoins);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(int amount)
    {
        if(TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(MONEY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
        }
        
    }
}
