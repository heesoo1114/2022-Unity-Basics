using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab; // 오리지날 프리팹을 저장해두고
    private Transform _parent; // 부모도 저장해서

    public Pool(T prefab, Transform parent, int count = 10)
    {

        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", ""); //Clone이름을 공백으로 제거
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }

    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }

    public T Pop()
    {
        T obj = null;
        if(_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }


}
