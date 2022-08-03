using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;

    public AIState positiveState; // 모든 조건이 참일 경우 일로 가고
    public AIState negativeState; // 조건중 하나라도 거짓일 경우 이쪽으로 가고


}
