using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseAction : AIAction
{
    private MeshRenderer _renderer;
    private NavMeshAgent _navMeshAgent;

    protected override void Awake()
    {
        base.Awake();
        _renderer = _brain.GetComponent<MeshRenderer>();
        _navMeshAgent = _brain.GetComponent<NavMeshAgent>();
    }

    public override void TakeAction()
    {
        _renderer.material.color = Color.red; // ChaseState¿¡¼­ »¡°£»ö
        _navMeshAgent.SetDestination(_brain.PlayerTrm.position);
    }
}
