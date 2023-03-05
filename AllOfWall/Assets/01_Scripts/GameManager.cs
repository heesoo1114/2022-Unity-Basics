using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject obsm;
    ObstacleManager obstacleManager;

    public static GameManager instance;
    public float SetTime;
    
    //[HideInInspector] 
    public int Score;

    public bool CollisionToLeft;
    public bool CollisionToRight;
    public bool CollisionToDown;
    public bool CollisionToUp;

    public bool isUpTime;
    public bool isRightTime;
    public bool isDownTime;
    public bool isLeftTime;

    public bool isTimerOn = false;

    public UnityEvent IsRightUI;
    public UnityEvent IsLeftUI;
    public UnityEvent IsDownUI;
    public UnityEvent IsUpUI;

    public UnityEvent BreakTime;

    public UnityEvent GameOverEvent;

    private void Awake()
    {
        Time.timeScale = 1;
        instance = this;
        obstacleManager = obsm.GetComponent<ObstacleManager>();
    }

    void Start()
    {
        // instance = this;
        Invoke("WhereIGo", 1f);
    }

    void Update()
    {
        if(isTimerOn == true)
        {
            if (SetTime <= 0)
            {
                isTimerOn = false;
                Defeat();
            }
            else if (SetTime > 0)
            {
                if (isUpTime) // 충돌했을 때
                {
                    if(CollisionToUp)
                    {
                        Win();
                    }
                    // 다시 확률 돌림
                }
                else if (isLeftTime) // 충돌했을 때
                {
                    if (CollisionToLeft)
                    {
                        Win();
                    }
                    // 다시 확률 돌림
                }
                else if (isRightTime) // 충돌했을 때
                {
                    if (CollisionToRight)
                    {
                        Win();
                    }
                    // 다시 확률 돌림
                }
                else if (isDownTime) // 충돌했을 때
                {
                    if (CollisionToDown)
                    {
                        Win();
                    }
                    // 다시 확률 돌림
                }
            }
            SetTime -= Time.deltaTime;
        }
    }

    public void TimeControll()
    {
        Time.timeScale = 0;
    }

    private void WhereIGo() // 어디로 가야하나
    {
        // CollisionReset();
        IsTimeReset();
        Obstacle.isDieTime = false;
        obstacleManager.Spawn();

        switch(WherePercentManager())
        {
            case 0:
            case 1:
                IsUPTime();
                break;
            case 2:
            case 3:
                IsLeftTime();
                break;
            case 4:
            case 5:
                IsRightTime();
                break;
            case 6:
            case 7:
                IsDownTime();
                break;
            case 8:
            case 9:
                WhereIGo();
                break;
        }
    }

    private int WherePercentManager()
    {
        int a = Random.Range(1, 99);
        int b = a % 10;
        return b;
    }

    private void CollisionReset()
    {
        CollisionToUp = false;
        CollisionToLeft = false;
        CollisionToUp = false;
        CollisionToUp = false;
    } // 충돌 초기화

    private void IsTimeReset() // 함수 실행 초기화
    {
        isUpTime = false;
        isLeftTime = false;
        isRightTime = false;
        isDownTime = false;
    }

    private void IsUPTime()
    {
        // print("GO TO UP");
        /*isTimerOn = true;
        isUpTime = true;*/
        if (CollisionToUp == false)
        {
            // CollisionReset();
            isTimerOn = true;
            isUpTime = true;
            IsUpUI?.Invoke();

            if (isTimerOn == false)
            {
                Defeat();
            }
        }
        else
        {
            WhereIGo();
        }
    }

    private void IsLeftTime()
    {
        // print("GO TO LEFT");
        /*isTimerOn = true;
        isLeftTime = true;*/
        if (!CollisionToLeft)
        {
            // CollisionReset();

            isTimerOn = true;
            isLeftTime = true;
            IsLeftUI?.Invoke();

            if (isTimerOn == false)
            {
                Defeat();
            }
        }
        else
        {
            WhereIGo();
        }
    }

    private void IsRightTime()
    {
        // print("GO TO RIGHT");
        /*isTimerOn = true;
        isRightTime = true;*/
        if (!CollisionToRight)
        {
            // CollisionReset();

            isTimerOn = true;
            isRightTime = true;
            IsRightUI?.Invoke();

            if (isTimerOn == false)
            {
                Defeat();
            }
        }
        else
        {
            WhereIGo();
        }
    }

    private void IsDownTime()
    {
        // print("GO TO DOWN");
        /*isTimerOn = true;
        isDownTime = true;*/
        if (!CollisionToDown)
        {
            // CollisionReset();

            isTimerOn = true;
            isDownTime = true;
            IsDownUI?.Invoke();

            if (isTimerOn == false)
            {
                Defeat();
            }
        }
        else
        {
            WhereIGo();
        }
    }

    private void Win()
    {
        BreakTime?.Invoke();
        isTimerOn = false;
        Score += 10;
        if(Score >= 0 && Score < 300)
        {
            obstacleManager.isFirst = true;
            obstacleManager.isSecond = false;
            obstacleManager.isThird = false;
        }
        else if (Score >= 300 && Score < 800)
        {
            obstacleManager.isSecond = true;
            obstacleManager.isFirst = false;
            obstacleManager.isThird = false;
        }
        else if (Score >= 800)
        {
            obstacleManager.isFirst = false;
            obstacleManager.isSecond = false;
            obstacleManager.isThird = true;
        }

        if(obstacleManager.isFirst)
        {
            SetTime = 7;
        }
        else if(obstacleManager.isSecond)
        {
            SetTime = 6;
        }
        else if(obstacleManager.isThird)
        {
            SetTime = 5;
        }
        obstacleManager.Reduce();
        IsTimeReset();
        // CollisionReset();
        // 다시 확률 돌림
        Invoke("WhereIGo", 1f);
    }

    private void Defeat()
    {
        GameOverEvent?.Invoke();
    }

    /*private void Defeat()
    {
        BreakTime?.Invoke();
        isTimerOn = false;
        Score -= 50;
        if(Score < 0)
        {
            GameOverEvent?.Invoke();
        }
        else if (Score >= 0 && Score < 300)
        {
            obstacleManager.isFirst = true;
            obstacleManager.isSecond = false;
            obstacleManager.isThird = false;
        }
        else if (Score >= 300 && Score < 800)
        {
            obstacleManager.isSecond = true;
            obstacleManager.isFirst = false;
            obstacleManager.isThird = false;
        }
        else if (Score >= 800)
        {
            obstacleManager.isFirst = false;
            obstacleManager.isSecond = false;
            obstacleManager.isThird = true;
        }
        SetTime = 5;
        obstacleManager.Reduce();
        IsTimeReset();
        // CollisionReset();
        // 다시 확률 돌림
        Invoke("WhereIGo", 1f);
    }*/
}
