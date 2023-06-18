using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleMaker2 : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [Header("ObstacleCntRange")]
    public int obstacleCntMin;
    public int obstacleCntMax;

    [Header("DelayRange")]
    public float delayTimeMin;
    public float delayTimeMax;

    [Header("PosRange")]
    [SerializeField] private float xPosMin;
    [SerializeField] private float xPosMax;

    // RealInfo
    private int obstacleCount;
    private float delayTime;
    private float xPos;

    public float RandomNumber(float min, float max) => UnityEngine.Random.Range(min, max);
    public int RandomNumber(int min, int max) => UnityEngine.Random.Range(min, max);

    private void Start()
    {
        StartCoroutine(MakeObstacle());
    }

    private void InfoSetting()
    {
        obstacleCount = RandomNumber(obstacleCntMin, obstacleCntMax);
        delayTime = RandomNumber(delayTimeMin, delayTimeMax);
    }

    IEnumerator MakeObstacle()
    {
        while (true)
        {
            InfoSetting();
            
            for (int i = 0; i < obstacleCount; i++)
            {
                GameObject prefab = Instantiate(obstaclePrefab);
                prefab.transform.parent = transform;

                xPos = RandomNumber(xPosMin, xPosMax);
                prefab.transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

                if (prefab.GetComponent<DownMovement>() == null)
                {
                    prefab.AddComponent<DownMovement>();
                }
                prefab.GetComponent<DownMovement>().MoveSpeed = 300;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

}
