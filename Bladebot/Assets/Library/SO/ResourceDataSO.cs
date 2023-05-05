using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate; // 드롭확률
    public PoolAbleMono ItemPrefab;

    public ResourceType ResourceType;
    public int MinAmount = 1, MaxAmount = 5;

    public AudioClip UseSound; // 아이템 먹었을 때 사운드
    public Color PopupTextColor; // 아이템 먹었을 때 뜨는 글자 색

    public int GetAmount()
    {
        return Random.Range(MinAmount, MaxAmount + 1);
    }
}
