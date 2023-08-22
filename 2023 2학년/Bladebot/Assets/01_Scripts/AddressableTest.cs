using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SubsystemsImplementation;

public class AddressableTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LoadEnemy();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyEnemy();
        }
    }

    private AsyncOperationHandle _handle;
    private GameObject _enemy;

    [SerializeField]
    private AssetReference _ref;

    private void LoadEnemy()
    {
        // Addressables.LoadAssetAsync<GameObject>("HammerEnemy").Completed += obj =>
        // {
        //     _handle = obj;
        //     _enemy = Instantiate(obj.Result, Vector3.zero, Quaternion.identity);
        // };  

        _ref.InstantiateAsync(Vector3.zero, Quaternion.identity).Completed += obj =>
        {
            _handle = obj;
            _enemy = obj.Result;
        };
    }
    
    private void DestroyEnemy()
    {
        Destroy(_enemy);
        Addressables.Release(_handle);
    }
}
