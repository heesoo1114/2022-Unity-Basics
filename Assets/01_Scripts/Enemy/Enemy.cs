using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;

    [SerializeField] private float moveSpeed = 3.0f;
    public float MoveSpeed { get; set; }
    // [SerializeField] private WayPoint waypoint;

    public WayPoint waypoint { get; set; }

    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWayPointIndex);

    private int _currentWayPointIndex;
    private Vector3 _lastPointPosition;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer _spriteRenderer; // flip

    public EnemyHealth EnemyHealth { get; set; }

    ObjPooler _pooler;

    private void Start()
    {
        _currentWayPointIndex = 0;
        _enemyHealth = GetComponent<EnemyHealth>();
        EnemyHealth = GetComponent<EnemyHealth>();

        MoveSpeed = moveSpeed;
        _lastPointPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Rotate();

        if(CheckPoint())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        // Vector3 currentPosition = waypoint.GetWaypointPosition(_currentWayPointIndex);
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;  
    }

    private bool CheckPoint()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if(distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    private void Rotate()
    {
        if(CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false; // 오른쪽 향하게
        }
        else
        {
            _spriteRenderer.flipX = true; // 왼쪽 향하게
        }
    }

    private void UpdateCurrentPointIndex()
    {
        int LastWaypointIndex = waypoint.Points.Length - 1;
        if(_currentWayPointIndex < LastWaypointIndex)
        {
            _currentWayPointIndex++;
        }
        else
        {
            // enemy가 끝까지 도착했다면 obj가 pooler로 되돌림
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWayPointIndex = 0;
    }
}
