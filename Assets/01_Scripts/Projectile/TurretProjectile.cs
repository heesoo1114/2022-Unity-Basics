using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPos;
    private ObjPooler _pooler;

    private void Start()
    {
        _pooler = GetComponent<ObjPooler>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            LoadProjectile();
        }
    }

    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPos.position;
        newInstance.transform.SetParent(projectileSpawnPos);
        newInstance.SetActive(true);
    }
}
