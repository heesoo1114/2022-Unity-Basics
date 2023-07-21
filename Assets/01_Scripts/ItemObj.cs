using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemObjType : int
{
    Head = 0,
    Body = 1,
    LeftHand = 2,
    RightHand = 3,
    Default = 4
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObj : ScriptableObject
{
    public ItemObjType itemObjType;
    public bool getFlagStackable;

    public Sprite itemIcon;
    public GameObject objModelPrefab;

    public ItemType itemData = new ItemType();
    public List<string> boneNameLists = new List<string>();

    [TextArea(15, 20)]
    public string itemSummery;

    // 유효성을 검사하는 함수
    private void OnValidate()
    {
        boneNameLists.Clear();

        if (objModelPrefab == null || objModelPrefab.GetComponent<SkinnedMeshRenderer>() == null )
        {
            return;
        }

        SkinnedMeshRenderer renderer = objModelPrefab.GetComponent<SkinnedMeshRenderer>();

        var bones = renderer.bones;

        foreach (var t in bones)
        {
            boneNameLists.Add(t.name);
        }
    }

    public ItemType CreateItemObj()
    {
        ItemType new_item = new ItemType(this);
        return new_item;
    }
}
