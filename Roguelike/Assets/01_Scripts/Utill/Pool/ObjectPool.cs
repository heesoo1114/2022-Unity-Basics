using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private ObjectPoolData[] objectPoolDatas;

    private readonly Dictionary<PoolObjectType, ObjectPoolData> poolObjectDataMap = new ();

    private readonly Dictionary<PoolObjectType, Queue<GameObject>> pool = new ();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        Init();
    }

    private void Init()
    {
        foreach (var data in objectPoolDatas)
        {
            poolObjectDataMap.Add(data.type, data);
        }

        foreach (var data in poolObjectDataMap)
        {
            pool.Add(data.Key, new Queue<GameObject>());

            for (int i = 0; i < data.Value.prefabCount; i++)
            {
                var poolObject = CreateNewObject(data.Key);

                pool[data.Key].Enqueue(poolObject);
            }
        }
    }

    private GameObject CreateNewObject(PoolObjectType objectType)
    {
        var newObj = Instantiate(poolObjectDataMap[objectType].prefab, transform, true);

        newObj.gameObject.SetActive(true);

        return newObj;
    }

    public GameObject GetObject(PoolObjectType type)
    {
        if (pool[type].Count > 0)
        {
            var obj = pool[type].Dequeue();
            obj.SetActive(true);

            return obj;
        }
        else
        {
            var newObj = CreateNewObject(type);
            newObj.SetActive(true);

            return newObj;
        }
    }

    public void ReturnObject(PoolObjectType type, GameObject obj)
    {
        obj.SetActive(false);
        pool[type].Enqueue(obj);
    }
}
