using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAIBrain _brain = null;

    [SerializeField]
    private List<AIAction> _actions = null;
    [SerializeField]
    private List<AITransition> _transition = null;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<EnemyAIBrain>();
    }

    public void UpDateState()
    {
        foreach(AIAction a in _actions)
        {
            a.TakeAction();
        }

        foreach(AITransition tr in _transition)
        {
            bool result = false;
            foreach(AIDecision d in tr.decisions)
            {
                result = d.MakeADecision();
                if (result == false) break;
            }
            
            if(result == true) // 이거는 해당 전이에 있는 모든 Decision 이 참이었다는거
            {
                 if(tr.positiveState != null)
                 {
                    _brain.ChangeState(tr.positiveState); // 에너미 브레인에게 통보를 함 -> positive로 변경하라는 통보
                    return;
                 }
            }
            else // 해당 전이에 있는 Decision중 하나가 거짓이었다는거
            {
                if(tr.negativeState != null)
                {
                    _brain.ChangeState(tr.negativeState); // 에너미 브레인에게 통보를 함 -> negative로 변경하라는 통보
                    return;
                }
            }
        }
    }
}
