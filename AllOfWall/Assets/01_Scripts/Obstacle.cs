using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Direction
{
    Right,
    Left,
    Up,
    Down
}

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    public Direction direction = Direction.Right;

    public static bool isDieTime = false;

    private void Start()
    {
        if(gameObject.activeSelf)
        {
            // 애니메이션
            if(direction == Direction.Right)
            {
                float a = transform.position.x - 0.6f;
                transform.DOMoveX(a, 1f);
            }
            else if(direction == Direction.Left)
            {
                float a = transform.position.x + 0.6f;
                transform.DOMoveX(a, 1f);
            }
            else if(direction == Direction.Up)
            {
                float a = transform.position.y - 0.6f;
                transform.DOMoveY(a, 1f);
            }
            else if(direction == Direction.Down)
            {
                float a = transform.position.y + 0.6f;
                transform.DOMoveY(a, 1f);
            }
        }
    }

    private void Update()
    {
        if(isDieTime)
        {
            ObstacleReduce();
        }
    }

    private void ObstacleReduce()
    {
        Destroy(gameObject);
    }
}
