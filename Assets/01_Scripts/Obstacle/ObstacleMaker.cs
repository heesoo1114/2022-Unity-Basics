using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaker : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacleList;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnDelayTime = 1.0f;

    public void MakeObstacle()
    {
        GameObject obsPrefab = Instantiate(obstaclePrefab);
        obsPrefab.transform.parent = GameObject.Find("BuildingContainer").transform;
        obsPrefab.transform.position = transform.position;
        obsPrefab.GetComponent<DownMovement>().MoveSpeed = 250;
    }
}
