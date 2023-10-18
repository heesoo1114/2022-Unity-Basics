using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System;
using System.ComponentModel.Design;

public class TestLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _levelArtRef;
    [SerializeField] private AssetReference _womanRef;
    [SerializeField] private AssetReference _zombieRef;

    private List<GameObject> list = new ();

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            LoadLevel();
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            DestroyAllList();
        }
    }

    private void DestroyAllList()
    {
        // 만들어진 에셋들을 전부 없애기

        foreach (var obj in list)
        {
            Destroy(obj);
            _levelArtRef.ReleaseInstance(obj);
        }
        _levelArtRef.ReleaseAsset();
        list.Clear();
    }

    private async void LoadLevel()
    {
        if (!_levelArtRef.IsValid())
        {
            await _levelArtRef.LoadAssetAsync<GameObject>().Task;
        }

        GameObject obj = Instantiate(_levelArtRef.Asset, Vector3.zero, Quaternion.identity) as GameObject;
        list.Add(obj);
    }
}
