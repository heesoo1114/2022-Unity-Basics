using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemManager : MonoBehaviour
{
    public GameItemContainer gameItemList;

    private void Start()
    {
        gameItemList = Resources.Load<GameItemContainer>("GameItemSO");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var gameItem in gameItemList.gameItemList)
            {
                Debug.Log($"{gameItem.itemName}, {gameItem.itemKind}, {gameItem.itemPrice}, {gameItem.itemIcon}");
            }
        }
    }
}
