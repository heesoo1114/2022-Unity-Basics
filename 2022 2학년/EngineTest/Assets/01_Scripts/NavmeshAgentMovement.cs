using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class NavmeshAgentMovement : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;

    private float _gravity = -9.8f;
    private float _verticalVelocity;
    private Vector3 _movementVelocity;

    private CharacterController _characterController;

    private AIActionData _aiActionData;

    protected void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _characterController = GetComponent<CharacterController>();
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetInitData(float speed)
    {
        _navAgent.speed = speed;
        _navAgent.isStopped = false; //1번추가
    }
    public bool CheckIsArrived()
    {
        //pathPending은 경로를 계산중일때 true 경로 계산이 모두 끝난중이면 false
        if (_navAgent.pathPending == false && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StopImmediately()
    {
        _navAgent.SetDestination(transform.position);
    }

    public void MoveToTarget(Vector3 pos)
    {
        _navAgent.SetDestination(pos);
    }

    public void StopNavigation()
    {
        _navAgent.isStopped = true;
    }

    public void ResetNavAgent()
    {
        _characterController.enabled = true;
        _navAgent.enabled = true;
        _navAgent.isStopped = false;
    }

    /*private void FixedUpdate()
    {
        if (_characterController.isGrounded == false)
        {
            _verticalVelocity = _gravity * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity = _gravity * 0.3f * Time.fixedDeltaTime;
        }
    }*/
}