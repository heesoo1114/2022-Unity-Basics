using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaker3 : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
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
            int obstacleCnt = RandomNumber(0, 3);

            for (int i = 0; i < obstacleCnt; i++)
            {
                GameObject prefab = Instantiate(obstaclePrefab);
                prefab.transform.parent = transform;

                int randomIndex = RandomNumber(obstacleCntMin, obstacleCntMax);
                prefab.transform.position = new Vector3(xPosList[randomIndex], transform.position.y, transform.position.z);

                if (prefab.GetComponent<DownMovement>() == null)
                {
                    prefab.AddComponent<DownMovement>();
                }
                prefab.GetComponent<DownMovement>().MoveSpeed = obstacleSpeed;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }
}
