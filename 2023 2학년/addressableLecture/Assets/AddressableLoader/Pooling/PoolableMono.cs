using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    public string assetGUID;
    public abstract void Init();
}
