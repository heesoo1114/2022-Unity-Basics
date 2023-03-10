using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IState
{
    public abstract void OnEnterState();

    public abstract void OnExitState();

    public abstract void UpdateState();

    protected AgentAnimator _agentAnimator;
    protected AgentInput _agnetInput;
    protected AgentController _agentController;

    public virtual void SetUp(Transform agentRoot)
    {
        _agentAnimator = agentRoot.Find("Visual").GetComponent<AgentAnimator>();
        _agnetInput = agentRoot.GetComponent<AgentInput>();
        _agentController = agentRoot.GetComponent<AgentController>();
    }

    public void OnHitHandle(Vector3 postion, Vector3 normal)
    {

    }
}
