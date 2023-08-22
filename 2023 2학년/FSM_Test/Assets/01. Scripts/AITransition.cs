using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecsion> Decisions;
    public AIState NextState;

    private void Awake()
    {
        Decisions = new List<AIDecsion>();
        GetComponents<AIDecsion>(Decisions);
    }

    public bool CheckTransition()
    {
        bool result = false;
        foreach (AIDecsion d in Decisions)
        {
            result = d.MakeDecision();
            if (d.IsReverse)
            {
                result = !result;
            }
            if (result == false) break;
        }
        return result;
    }
}
