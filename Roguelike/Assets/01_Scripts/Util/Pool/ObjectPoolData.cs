using System;
using UnityEngine;

[Serializable]
public class ObjectPoolData
{
    public GameObject prefab;

    public PoolObjectType ObjectType;

    public int prefabCount;
}