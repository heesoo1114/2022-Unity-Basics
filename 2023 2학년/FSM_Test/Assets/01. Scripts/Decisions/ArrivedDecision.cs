using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

public class ArrivedDecision : AIDecsion
{
    private NavMeshAgent _agent;

    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponentInParent<NavMeshAgent>();
    }

    public override bool MakeDecision()
    {
        return Vector3.Distance(_agent.destination, transform.position) < 0.2f;
    }
}
