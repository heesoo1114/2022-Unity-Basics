using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action OnEndReached;

    [SerializeField] private float moveSpeed = 3.0f;
    // [SerializeField] private WayPoint waypoint;

    public WayPoint waypoint { get; set; }

    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWayPointIndex);

    private int _currentWayPointIndex;

    private EnemyHealth _enemyHealth;

    ObjPooler _pooler;

    private void Start()
    {
        _currentWayPointIndex = 0;
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        /*if (_currentWayPointIndex == waypoint.Points.Length)
        {
            return;
        }*/

        Move();
        if(CheckPoint())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        // Vector3 currentPosition = waypoint.GetWaypointPosition(_currentWayPointIndex);
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
    }

    private bool CheckPoint()
    {
        /*if (CurrentPointPosition == transform.position)
        {
            _currentWayPointIndex++;
        }*/

        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if(distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
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
            // enemy�� ������ �����ߴٸ� obj�� pooler�� �ǵ���
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke();
        _enemyHealth.ResetHealth();
        ObjPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWayPointIndex = 0;
    }
}
