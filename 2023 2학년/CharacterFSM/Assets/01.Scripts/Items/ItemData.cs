using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(menuName = "SO/Items/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string itemID;

    protected StringBuilder _stringBuilder = new StringBuilder();

#if UNITY_EDITOR
    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this); // 이 에셋의 현재 경로
        itemID =  AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}
