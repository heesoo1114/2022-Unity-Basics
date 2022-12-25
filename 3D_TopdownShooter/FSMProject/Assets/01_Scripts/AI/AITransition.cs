using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions; // 결정 사항들을 가지고 있는거

    public AIState positiveResult; // 모든 디시전이 true 라면 갈 곳

    public AIState negativeResult; // 디시전 중 하나라도 true가 아니라면 갈 곳
}
