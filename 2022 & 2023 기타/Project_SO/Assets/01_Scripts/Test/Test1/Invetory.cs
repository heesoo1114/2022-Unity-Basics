using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invetory : MonoBehaviour
{
    public GameItemContainer gameItemList;

    private void Start()
    {
        gameItemList = Resources.Load<GameItemContainer>("GameItemSO");

        foreach (var gameItem in gameItemList.gameItemList)
        {
            Debug.Log($"{gameItem.itemName}, {gameItem.itemKind}, {gameItem.itemPrice}, {gameItem.itemIcon}");
        }
    }
}
