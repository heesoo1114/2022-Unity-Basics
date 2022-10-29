using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField]
    private AIState _currentState;

    public Transform target = null;

    private void Start()
    {
        target = GameManager.Instance.PlayerTrm;
    }

    public void ChangeToState(AIState nextState)
    {
        _currentState = nextState;
    }

    protected virtual void Update()
    {
        _currentState.UpdateState();
    }

    public virtual void Attack()
    {
        Debug.Log("공격");
    }

    public void Move(Vector2 direction, Vector2 targetPos)
    {
        Debug.Log("이동");
    }
}
