using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField]
    private AIState _aiState;

    public void ChangeState(AIState nextState)
    {
        _aiState = nextState; // ���� ��ȯ
    }

    private void Update()
    {
        _aiState?.UpdateState();
    }
}
