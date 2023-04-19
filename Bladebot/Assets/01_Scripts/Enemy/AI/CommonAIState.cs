using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CommonAIState : MonoBehaviour, IState
{
    protected List<AITransition> _transitions;

    protected EnemyController _enemyController;
    protected AIActionData _aiActionData;

    public abstract void OnEnterState();
    public abstract void OnExitState();

    public virtual void SetUp(Transform agentRoot)
    {
        _enemyController = agentRoot.GetComponent<EnemyController>();
        _aiActionData = agentRoot.Find("AI").GetComponent<AIActionData>();

        _transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(_transitions);

        _transitions.ForEach(t => t.SetUp(agentRoot)); //여기서부터 셋업이 연쇄한다.
    }

    public virtual void UpdateState()
    {
        foreach (AITransition t in _transitions)
        {
            if (t.CheckDecisions())
            {
                _enemyController.ChangeState(t.NextState);
                break;
            }
        }
    }
}