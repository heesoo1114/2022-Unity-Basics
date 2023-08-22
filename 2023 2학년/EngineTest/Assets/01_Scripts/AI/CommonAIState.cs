using System.Collections;
using System.Collections.Generic;
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

    public virtual bool UpdateState()
    {
        foreach (AITransition t in _transitions)
        {
            if (t.CheckDecisions())
            {
                _enemyController.ChangeState(t.NextState);
                return true;
            }
        }

        // AnyState 상태 전이 검사
        foreach (AITransition t in _enemyController.AnyTransitions)
        {
            if (t.CheckDecisions())
            {
                _enemyController.ChangeState(t.NextState);
                return true;
            }
        }

        return false;
    }
}