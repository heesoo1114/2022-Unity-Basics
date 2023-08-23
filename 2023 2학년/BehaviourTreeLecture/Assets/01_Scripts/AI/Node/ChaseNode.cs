using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class ChaseNode : Node
{
    private NavMeshAgent _agent;
    private Transform _target;
    private EnemyBrain _brain;

    public ChaseNode(Transform target, NavMeshAgent agent, EnemyBrain brain)
    {
        _target = target;
        _agent = agent;
        _brain = brain;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_target.position, _agent.transform.position);
        if (distance > 0.2f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);

            if (_nodeState != NodeState.RUNNING)
            {
                _brain.TryToTalk("Chasing!!", 3f);
                _nodeState = NodeState.RUNNING;
            }
        }
        else
        {
            _agent.isStopped = false;
            _nodeState = NodeState.SUCCESS;
        }
        return _nodeState;
    }
}
