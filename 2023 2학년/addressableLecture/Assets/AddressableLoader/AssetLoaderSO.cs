using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AssetName
{
    LineTrail = 0,
    AudioEffect = 1,
}

[Serializable]
public struct PoolingItem
{
    public AssetReference assetRef;
    public ushort count;
}

[CreateAssetMenu(menuName = "SO/AddressableAssets")]
public class AssetLoaderSO : ScriptableObject
{
    public List<AssetReference> loadingList; // �ܼ��� �ε��� �ϸ� �Ǵ� �ֵ�
    public List<PoolingItem> poolingList;    // Ǯ���� �ؾ��ϴ� �ֵ�
    
    public int TotalCount => loadingList.Count + poolingList.Count; // �ε��� ���� ����

    private Dictionary<string, AssetReference> _nameDictionary;
    private Dictionary<string, AssetReference> _guidDictionary;

    private void OnEnable()
    {
        _nameDictionary = new ();
        _guidDictionary = new ();
    }

    public void LoadingComplete(AssetReference reference, string name)
    {
        _guidDictionary.Add(reference.AssetGUID, reference);
        _nameDictionary.Add(name, reference);
    }

    public UnityEngine.Object GetAsset(string guid)
    {
        if (_guidDictionary.TryGetValue(guid, out AssetReference value))
        {
            return value.Asset;
        }
        return null;
    }

    public UnityEngine.Object GetAssetByName(string name)
    {
        if (_nameDictionary.TryGetValue(name, out AssetReference value))
        {
            return value.Asset;
        }
        return null;
    }
}
