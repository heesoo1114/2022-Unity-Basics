using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaker : MonoBehaviour
{
    public List<GameObject> cloudList;
    public List<float> xPosList;

    [Header("[LevelDesign]")]
    public int obstacleCntMin;
    public int obstacleCntMax;
    public float obstacleSpeed;
    public float delayTime;

    public int RandomNumber(int min, int max) => UnityEngine.Random.Range(min, max);

    public void Init()
    {
        StopAllCoroutines();

        // 인스펙터에서 바꾸면 여기서도 바꿔주어야 함
        obstacleSpeed = 300;
        delayTime = 1;
    }

    public void RoopStop()
    {
        StopAllCoroutines();
    }

    public void LevelUpProperty()
    {
        obstacleSpeed *= 1.05f;
        delayTime *= 0.98f;  

        foreach (GameObject obj in cloudList)
        {
            obj.GetComponent<CloudMovement>().MoveSpeed = obstacleSpeed;
        }
    }

    public void StartMakeRoop()
    {
        Init();
        StartCoroutine(MakeRoop());
    }

    IEnumerator MakeRoop()
    {
        while (true)
        {
            int obstacleCnt = RandomNumber(obstacleCntMin, obstacleCntMax);

            for (int i = 0; i < obstacleCnt; i++)
            {
                Building prefab = PoolManager.Instance.Pop("Building") as Building;

                int randomIndex = RandomNumber(0, 3);
                prefab.transform.position = new Vector3(xPosList[randomIndex], transform.position.y, transform.position.z);

                prefab.GetComponent<Building>().MoveSpeed = obstacleSpeed;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }
}
