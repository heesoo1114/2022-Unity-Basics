using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AITransition> Transitions = null;

    private AIBrain _brain;

    private void Awake()
    {
        Transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(Transitions);
        
        _brain = transform.GetComponentInParent<AIBrain>();
    }

    public void UpdateState()
    {
        foreach (AITransition t in Transitions)
        {
            if (t.CheckTransition())
            {
                // 상태 전환 시작

                _brain.ChangeState(t.NextState);

                break;
            }
        }
    }
}
