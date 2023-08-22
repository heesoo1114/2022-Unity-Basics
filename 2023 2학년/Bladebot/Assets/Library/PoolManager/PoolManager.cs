using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance;

    private Dictionary<string, Pool<PoolAbleMono>> _pools = new Dictionary<string, Pool<PoolAbleMono>>();

    private Transform _trmParent;

    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }

    public void CreatePool(PoolAbleMono prefab, int count = 10)
    {
        // 해당 게임오브젝트의 이름을 기반으로 풀을 만들어서 관리한다.
        Pool<PoolAbleMono> pool = new Pool<PoolAbleMono>(prefab, _trmParent, count);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public PoolAbleMono Pop(string prefabName)
    {
        if (!_pools.ContainsKey(prefabName))
        {
            Debug.Log($"Prefab does not exist on pool : {prefabName}");
            return null;
        }
        PoolAbleMono item = _pools[prefabName].Pop();
        item.Init();
        return item;
    }

    public void Push(PoolAbleMono obj)
    {
        _pools[obj.name].Push(obj);
    }
}
