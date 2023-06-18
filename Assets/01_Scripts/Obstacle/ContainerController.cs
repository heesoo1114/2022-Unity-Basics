using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    ObstacleMaker _obsMaker;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveRange;
    [SerializeField] private float arrivalThreshold;

    private float targetPos;

    private Vector3 moveDir;

    private void Awake()
    {
        _obsMaker = GetComponent<ObstacleMaker>();
    }

    private void Start()
    {
        SetTargetPos();
    }

    private void SetTargetPos()
    {
        // 초기 위치 설정
        targetPos = Random.Range(-moveRange, moveRange);
        Debug.Log("new pos " + targetPos);
        SetMoveDir();

        // 현재 게임 플레이 테스트 중
        _obsMaker.MakeObstacle();
    }

    private void SetMoveDir()
    {
        if (targetPos > transform.position.x)
        {
            moveDir = Vector3.right;
        }
        else
        {
            moveDir = Vector3.left;
        }
    }

    private void Update()
    {
        // 목적지(targetPos에 도착하였으면)
        if (Mathf.Abs(transform.position.x - targetPos) <= arrivalThreshold)
        {
            SetTargetPos();
        }

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }

}
