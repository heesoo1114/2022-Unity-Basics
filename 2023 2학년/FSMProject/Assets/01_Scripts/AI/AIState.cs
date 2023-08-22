using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    // 내가 이 상태에서 수행해야할 액션들을 여기서 가지고 있는다
    public List<AIAction> actions = null;

    // 내가 이 상태에서 전이가 가능한 상태로의 전이 리스트들
    public List<AITransition> transitions = null;

    private AIBrain _brain;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public void UpdateState()
    {
        // 모든 상태는 매 프레임마다 이 메서드를 실행한다
        foreach (AIAction a in actions)
        {
            a.TakeAction();
        }

        foreach (AITransition t in transitions)
        {
            bool result = false;

            foreach (AIDecision d in t.decisions)
            {
                result = d.MakeADecision(); // 결정해라
                if (!result) break;
            }

            if (result) // 모든 디시전을 만족하는 상태니까 전이를 해야한다.
            {
                if (t.positiveResult != null)
                {
                    // positiveResult로 전이를 발생
                    _brain.ChangeToState(t.positiveResult);
                }
            }
            else
            {
                if (t.negativeResult != null)
                {
                    // negativeResult로 전이를 해야함
                    _brain.ChangeToState(t.negativeResult);
                }
            }
        }
    }
}
