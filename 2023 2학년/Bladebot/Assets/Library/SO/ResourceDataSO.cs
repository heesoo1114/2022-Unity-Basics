using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate; // ���Ȯ��
    public PoolAbleMono ItemPrefab;

    public ResourceType ResourceType;
    public int MinAmount = 1, MaxAmount = 5;

    public AudioClip UseSound; // ������ �Ծ��� �� ����
    public Color PopupTextColor; // ������ �Ծ��� �� �ߴ� ���� ��

    public int GetAmount()
    {
        return Random.Range(MinAmount, MaxAmount + 1);
    }
}
