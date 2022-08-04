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
            
            if(result == true) // �̰Ŵ� �ش� ���̿� �ִ� ��� Decision �� ���̾��ٴ°�
            {
                 if(tr.positiveState != null)
                 {
                    _brain.ChangeState(tr.positiveState); // ���ʹ� �극�ο��� �뺸�� �� -> positive�� �����϶�� �뺸
                    return;
                 }
            }
            else // �ش� ���̿� �ִ� Decision�� �ϳ��� �����̾��ٴ°�
            {
                if(tr.negativeState != null)
                {
                    _brain.ChangeState(tr.negativeState); // ���ʹ� �극�ο��� �뺸�� �� -> negative�� �����϶�� �뺸
                    return;
                }
            }
        }
    }
}
