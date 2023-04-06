using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    private MeshRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        _renderer = _brain.GetComponent<MeshRenderer>();
    }

    public override void TakeAction()
    {
        _renderer.material.color = Color.blue; // IdleState에서 파란색
    }
}
