using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
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
    
    void Start()
    {
        instance = this;
        WhereIGo();
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
                if (isUpTime) // �浹���� ��
                {
                    if(CollisionToUp)
                    {
                        Win();
                    }
                    // �ٽ� Ȯ�� ����
                }
                else if (isLeftTime) // �浹���� ��
                {
                    if (CollisionToLeft)
                    {
                        Win();
                    }
                    // �ٽ� Ȯ�� ����
                }
                else if (isRightTime) // �浹���� ��
                {
                    if (CollisionToRight)
                    {
                        Win();
                    }
                    // �ٽ� Ȯ�� ����
                }
                else if (isDownTime) // �浹���� ��
                {
                    if (CollisionToDown)
                    {
                        Win();
                    }
                    // �ٽ� Ȯ�� ����
                }
            }
            SetTime -= Time.deltaTime;
        }
    }

    private void WhereIGo() // ���� �����ϳ�
    {
        // CollisionReset();
        IsTimeReset();

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
    } // �浹 �ʱ�ȭ

    private void IsTimeReset() // �Լ� ���� �ʱ�ȭ
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

            if (isTimerOn == false)
            {
                print("Defeat");
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

            if (isTimerOn == false)
            {
                print("Defeat");
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

            if (isTimerOn == false)
            {
                print("Defeat");
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

            if (isTimerOn == false)
            {
                print("Defeat");
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
        print("Win");
        isTimerOn = false;
        Score += 10;
        SetTime = 5;
        IsTimeReset();
        // CollisionReset();
        // �ٽ� Ȯ�� ����
        Invoke("WhereIGo", 0.7f);
    }

    private void Defeat()
    {
        print("Defeat");
        isTimerOn = false;
        Score -= 5;
        SetTime = 5;
        IsTimeReset();
        // CollisionReset();
        // �ٽ� Ȯ�� ����
        Invoke("WhereIGo", 0.7f);
    }
}
