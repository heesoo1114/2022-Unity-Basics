using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolingPair
{
    public PoolAbleMono prefab;
    public int poolCount;
}

[CreateAssetMenu(menuName = "SO/System/PoolingList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingPair> list;
}
