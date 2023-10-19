using System.Collections.Generic;
using UnityEngine;

class Pool<T> where T : PoolableMono
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab; //오리지날 저장
    private string _itemGUID;
    private Transform _parent;

    public Pool(string guid, T prefab, Transform parent, int count = 10)
    {
        _prefab = prefab;
        _parent = parent;
        _itemGUID = guid;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.assetGUID = guid;
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public void Clear()
    {
        while (_pool.Count > 0)
        {
            var item = _pool.Pop();
            GameObject.Destroy(item.gameObject);
        }
    }

    public T Pop()
    {
        T obj = null;
        if (_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.assetGUID = _itemGUID;
        }
        else
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}