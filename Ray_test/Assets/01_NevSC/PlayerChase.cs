using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChase : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private NavMeshAgent _navAgent;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _navAgent.SetDestination(_target.position);
        }
    }
}
