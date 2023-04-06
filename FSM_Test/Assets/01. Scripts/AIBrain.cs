using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField]
    private AIState _aiState;

    [SerializeField]
    private Transform _playerTrm;
    public Transform PlayerTrm => _playerTrm;

    public void ChangeState(AIState nextState)
    {
        _aiState = nextState; // 상태 전환
    }

    private void Update()
    {
        _aiState?.UpdateState();
    }
}
