using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabB : MonoBehaviour
{
    public ShareDB shareDB;

    private void Start()
    {
        shareDB = Resources.Load<ShareDB>("ShareDB");

        Debug.Log("Prefab B에서 공유 한 값 " + shareDB.SharedValue);

        shareDB.SharedValue += 10;
    }
}
