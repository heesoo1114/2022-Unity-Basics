using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤 관리는 GameManager가 함
public class PoolManager
{

    public static PoolManager Instance = null;

    private Dictionary<string, Pool<PoolAbleMono>> _pools = new Dictionary<string, Pool<PoolAbleMono>>();

    private Transform _trmParent; // 모든 풀링 오브젝트들의 부모
    public PoolManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }

    public void CreatePool(PoolAbleMono prefab, int count = 10)
    {
        Pool<PoolAbleMono> pool = new Pool<PoolAbleMono>(prefab, _trmParent, count);
        _pools.Add(prefab.gameObject.name, pool);
        // 딕셔너리에 프리팹의 이름으로 pool을 등록한다.
    }

    public void Push(PoolAbleMono obj)
    {
        if (_pools.ContainsKey(obj.gameObject.name))
            _pools[obj.gameObject.name].Push(obj);
        else
            Debug.LogError($"{ obj.gameObject.name}은 풀에 존재하지 않습니다.");
    }

    public PoolAbleMono Pop(string objName)
    {
        if (_pools.ContainsKey(objName) == false)
        {
            Debug.LogError($"Pop Error : {objName}이라는 풀은 없습니다.");
            return null;
        }
        PoolAbleMono item = _pools[objName].Pop();
        item.Init(); // 초기화 해주고
        return item;
    }

}
