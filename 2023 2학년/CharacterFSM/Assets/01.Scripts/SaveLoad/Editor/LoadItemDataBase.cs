using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class LoadItemDataBase : Editor
{
    private SaveManager _saveManager;
    private string _soFilename = "ItemDB";

    private void OnEnable()
    {
        _saveManager = (SaveManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate ITem DB"))
        {
            CreateAssetDataBase();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void CreateAssetDataBase()
    {
        List<ItemData> loadedItemList = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/08.SO/Items" });

        foreach (string assetName in assetNames)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); // GUID를 기반으로 path
            ItemData itemData = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);
            if (itemData != null)
            {
             loadedItemList.Add(itemData);
            }
        }

        string filename = $"Assets/08.SO/{_soFilename}.asset";
        ItemDatabaseSO itemDB = AssetDatabase.LoadAssetAtPath<ItemDatabaseSO>(filename);

        if (itemDB == null)
        {
            itemDB = ScriptableObject.CreateInstance<ItemDatabaseSO>();
            itemDB.itemList = loadedItemList;
            string fullPath = AssetDatabase.GenerateUniqueAssetPath(filename);  
            AssetDatabase.CreateAsset(itemDB, fullPath);
            Debug.Log($"item DB create at {fullPath}");            
        }
        else
        {
            itemDB.itemList = loadedItemList;
            EditorUtility.SetDirty(itemDB); // 지저분해졌으니 저장해
            Debug.Log("item DB updated");
        }
    }

}
