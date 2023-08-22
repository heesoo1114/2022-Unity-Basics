using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    private List<AIDecision> _decisions;

    [SerializeField]
    private CommonAIState _nextState;
    public CommonAIState NextState => _nextState;

    public void SetUp(Transform parentRoot)
    {
        _decisions = new List<AIDecision>();
        GetComponents<AIDecision>(_decisions); // 모든 디시전 가져오고
        _decisions.ForEach(d => d.SetUp(parentRoot)); // 가지고 있는 모든 디시전들 셋업
    }

    public bool CheckDecisions()
    {
        bool result = false;

        foreach (AIDecision d in _decisions)
        {
            result = d.MakeDecision();

            if (d.IsReverse) result = !result;
            if (result == false) break;
        }

        return result;
    }
}
