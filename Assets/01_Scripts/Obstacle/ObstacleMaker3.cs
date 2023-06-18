using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMaker3 : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    public List<float> xPosList;
    
    public float delayTime;

    public int RandomNumber(int min, int max) => UnityEngine.Random.Range(min, max);

    private void Start()
    {
        // StartCoroutine(MakeRoop());
    }

    IEnumerator MakeRoop()
    {
        while (true)
        {
            int obsCnt = RandomNumber(1, 2);

        }
    }
}
