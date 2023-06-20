using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaker : MonoBehaviour
{
    // [SerializeField] private GameObject obstaclePrefab;

    public List<GameObject> buildingList;
    public List<float> xPosList;

    [Header("[LevelDesign]")]
    public int obstacleCntMin;
    public int obstacleCntMax;
    public int obstacleSpeed;
    public float delayTime;

    public int RandomNumber(int min, int max) => UnityEngine.Random.Range(min, max);

    private void Start()
    {
        StartCoroutine(MakeRoop());
    }

    IEnumerator MakeRoop()
    {
        while (true)
        {
            int obstacleCnt = RandomNumber(1, 3);

            for (int i = 0; i < obstacleCnt; i++)
            {
                Building prefab = PoolManager.Instance.Pop("Building") as Building;

                int randomIndex2 = RandomNumber(obstacleCntMin, obstacleCntMax);
                prefab.transform.position = new Vector3(xPosList[randomIndex2], transform.position.y, transform.position.z);

                prefab.GetComponent<Building>().MoveSpeed = obstacleSpeed;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }
}
