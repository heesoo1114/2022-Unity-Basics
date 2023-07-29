using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool Instance;
    
    [SerializeField] private ObjectPoolData[] originPoolObjectData;

    private readonly Dictionary<PoolObjectType, ObjectPoolData> _objectPoolDataMap = new();
    
    private readonly Dictionary<PoolObjectType, Queue<GameObject>> _pool = new();

    protected void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        Initialize();
    }

    private void Initialize()
    {
        foreach (var originData in originPoolObjectData)
            _objectPoolDataMap.Add(originData.ObjectType, originData);
        
        foreach (var poolObjectData in _objectPoolDataMap)
        {
            _pool.Add(poolObjectData.Key, new Queue<GameObject>());

            for (var j = 0; j < poolObjectData.Value.prefabCount; j++)
            {
                var poolObject = CreateNewObject(poolObjectData.Key);

                _pool[poolObjectData.Key].Enqueue(poolObject);
            }
        }
    }

    private GameObject CreateNewObject(PoolObjectType prefab)
    {
        var newObj = Instantiate(_objectPoolDataMap[prefab].prefab, transform, true);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public GameObject GetObject(PoolObjectType type)
    {
        if (_pool[type].Count > 0)
        {
            var obj = _pool[type].Dequeue();
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject(type);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(transform);

            return newObj;
        }
    }

    public void ReturnObject(PoolObjectType type, GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        
        _pool[type].Enqueue(obj);
    }
    
}