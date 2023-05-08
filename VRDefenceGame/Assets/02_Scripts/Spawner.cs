using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public bool playOnStart = true;
    public float startFactor = 1f;
    public float additiveFactor = 0.1f;
    public float delayPerSpawnGroup = 3f;

    private void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }

    private void Play()
    {
        StartCoroutine(Process());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        var factor = startFactor;
        var wfs = new WaitForSeconds(delayPerSpawnGroup);

        while (true)
        {
            yield return wfs;
            yield return StartCoroutine(SpawnProcess(factor));

            factor += additiveFactor;
        }
    }

    private IEnumerator SpawnProcess(float factor)
    {
        var count = UnityEngine.Random.Range(factor, factor * 2f);
        for (int i = 0; i < count; i++)
        {
            Spawn();

            if (UnityEngine.Random.value < 0.2)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.02f));
            }
        }
    }

    private void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
