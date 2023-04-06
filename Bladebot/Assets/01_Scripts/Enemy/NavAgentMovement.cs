using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    public NavMeshAgent NavMeshAgent => _navAgent;

    protected void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public bool CheckIsArrived()
    {
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
}