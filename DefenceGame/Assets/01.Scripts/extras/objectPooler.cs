using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    [SerializeField]//뭘 preload 할 것인지
    private GameObject prefab;
    [SerializeField]//pooler 크기
    private int poolSize = 10;

    private List<GameObject> _pool; //배열로 만들어서 오브젝트 관리
    private GameObject _poolContainer; //pool에서 만든 오브젝트 구조화

    private void Awake()
    {
        _pool = new List<GameObject>();

        _poolContainer = new GameObject(name: $"Pool-{prefab.name}");

        CreatePooler();
    }

    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(item: CreateInstance());
        }
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_poolContainer.transform);

        newInstance.SetActive(false);

        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return CreateInstance();
    }

    //enemy를 pool로 되돌리는 메소드
    public static void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
    }

    public static IEnumerator ReturnToPoolwthDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }


}

