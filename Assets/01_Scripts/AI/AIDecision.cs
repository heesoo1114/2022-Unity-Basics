using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIBrain _brain;

    private void Awake()
    {
        _brain = transform.parent.parent.parent.GetComponent<AIBrain>();
    }

    public abstract bool MakeADecision(); // 결정을 내려라
}
