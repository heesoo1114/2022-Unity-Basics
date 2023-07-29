using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStateDecision : AIDecision
{
    [SerializeField]
    private CommonAIState _baseState;

    public override bool MakeDecision()
    {
        return _enemyController.CurrentState == _baseState;
    }
}
