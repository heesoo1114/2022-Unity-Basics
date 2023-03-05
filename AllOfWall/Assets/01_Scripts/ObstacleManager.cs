using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("SpawnPosition")]
    [SerializeField] private Vector3[] UpSpawnPosition;
    [SerializeField] private Vector3[] DownSpawnPosition;
    [SerializeField] private Vector3[] RightSpawnPosition;
    [SerializeField] private Vector3[] LeftSpawnPosition;

    [Header("ObstaclePrefab")]
    [SerializeField] private GameObject[] ObstacleUPrefab;
    [SerializeField] private GameObject[] ObstacleDPrefab;
    [SerializeField] private GameObject[] ObstacleLPrefab;
    [SerializeField] private GameObject[] ObstacleRPrefab;

    // [SerializeField] private int[] ObstacleSpawnPercent;

    // 나중에 static 추가
    public bool isFirst = true;
    public bool isSecond = false;
    public bool isThird = false;

    private int a;

    public GameObject[] prefab;

    private void Start()
    {
        
    }

    public void Spawn()
    {
        SpawnUDManager();
        SpawnLRManager();
        SpawnWhatPrefab(); 
    }

    public void Reduce()
    {
        Obstacle.isDieTime = true;
    }

    private int SpawnWhatPrefab()
    {
        if (isFirst)
        {
            switch(SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    a = 0;
                    break;
                case 6:
                case 7:
                case 8:
                    a = 1;
                    break;
                case 9:
                    a = 2;
                    break;
            }
        }
        else if(isSecond)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    a = 0;
                    break;
                case 4:
                case 5:
                case 6:
                    a = 1;
                    break;
                case 7:
                case 8:
                    a = 2;
                    break;
                case 9:
                    a = 3;
                    break;
            }
        }
        else if(isThird)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                    a = 0;
                    break;
                case 2:
                case 3:
                case 4:
                    a = 1;
                    break;
                case 5:
                case 6:
                case 7:
                    a = 2;
                    break;
                case 8:
                case 9:
                    a = 3;
                    break;
            }
        }
        return a;
    }

    public int SpawnPercentManager()
    {
        int a = Random.Range(1, 99);
        int b = a % 10;
        return b;
    }

    private void SpawnUDManager()
    {
        if(isFirst)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    {
                        SingleUDSpawn();
                    }
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    {
                        DoubleUDSpawn();
                    }
                    break;
            }
        }
        else if(isSecond)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    SingleUDSpawn();
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    DoubleUDSpawn();
                    break;
                case 8:
                case 9:
                    TripleUDSpawn();
                    break;
            }
        }
        else if(isThird)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                    SingleUDSpawn();
                    break;
                case 3:
                case 4:
                    DoubleUDSpawn();
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    TripleUDSpawn();
                    break;
            }
        }
        
    }

    private void SpawnLRManager()
    {
        if(isFirst)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    {
                        SingleLRSpawn();
                    }
                    break;
            }
        }
        else if(isSecond)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    SingleLRSpawn();
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    DoubleLRSpawn();
                    break;
            }
        }
        else if(isThird)
        {
            switch (SpawnPercentManager())
            {
                case 0:
                case 1:
                case 2:
                    SingleLRSpawn();
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    DoubleLRSpawn();
                    break;
            }
        }
    }
    
    private void SingleUDSpawn()
    {
        // 랜덤 위치 -6 부터 10까지
        if(!PlayeyContoller.isDown) // 다운에 있을 때 다운스폰 ㄴㄴ
        {
            Instantiate(ObstacleDPrefab[SpawnWhatPrefab()], DownSpawnPosition[2], Quaternion.identity);
        }
        if(!PlayeyContoller.isUp)
        {
            Instantiate(ObstacleUPrefab[SpawnWhatPrefab()], UpSpawnPosition[2], Quaternion.identity);
        }
    }

    private void SingleLRSpawn()
    {
        // 랜덤 위치 -6 부터 10까지
        if(!PlayeyContoller.isLeft)
        {
            Instantiate(ObstacleLPrefab[SpawnWhatPrefab()], LeftSpawnPosition[1], Quaternion.identity);
        }
        if(!PlayeyContoller.isRight)
        {
            Instantiate(ObstacleRPrefab[SpawnWhatPrefab()], RightSpawnPosition[1], Quaternion.identity);
        }
    }

    private void DoubleUDSpawn()
    {
        for (int i = 0; i < DownSpawnPosition.Length; i++)
        {
            if (i == 1 || i == 3)
            {
                if (!PlayeyContoller.isDown)
                {
                    Instantiate(ObstacleDPrefab[SpawnWhatPrefab()], DownSpawnPosition[i], Quaternion.identity);
                }
                if (!PlayeyContoller.isUp)
                {
                    Instantiate(ObstacleUPrefab[SpawnWhatPrefab()], UpSpawnPosition[i], Quaternion.identity);
                }
            }
        }
    }

    private void DoubleLRSpawn()
    {
        for (int i = 0; i < DownSpawnPosition.Length; i++)
        {
            if (i == 0 || i == 2)
            {
                if (!PlayeyContoller.isLeft)
                {
                    Instantiate(ObstacleLPrefab[SpawnWhatPrefab()], LeftSpawnPosition[i], Quaternion.identity);
                }
                if (!PlayeyContoller.isRight)
                {
                    Instantiate(ObstacleRPrefab[SpawnWhatPrefab()], RightSpawnPosition[i], Quaternion.identity);
                }
            }
        }
    }

    private void TripleUDSpawn()
    {
        for(int i = 0; i < DownSpawnPosition.Length; i++)
        {
            if(i == 0 || i == 2 || i == 4)
            {
                if(!PlayeyContoller.isDown)
                {
                    Instantiate(ObstacleDPrefab[SpawnWhatPrefab()], DownSpawnPosition[i], Quaternion.identity);
                }
                if(!PlayeyContoller.isUp)
                {
                    Instantiate(ObstacleUPrefab[SpawnWhatPrefab()], UpSpawnPosition[i], Quaternion.identity);
                }
            }
        }
    }
}
