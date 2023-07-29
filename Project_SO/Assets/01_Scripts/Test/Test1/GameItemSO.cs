using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test/GameItemSO")]
public class GameItemContainer : ScriptableObject
{
    public List<GameItemSO> gameItemList;
}

[System.Serializable]
public class GameItemSO
{
    public string itemName;
    public string itemKind;
    public int itemPrice;
    public Sprite itemIcon;
}
