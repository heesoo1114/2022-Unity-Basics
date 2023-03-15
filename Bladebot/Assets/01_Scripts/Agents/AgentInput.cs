using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnAttackKeyPress = null;

    

    private void Update()
    {
        UpdateMoveIpnut();
        UpdateAttackInput();
    }

    private void UpdateAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackKeyPress?.Invoke();
        }
    }

    private void UpdateMoveIpnut()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector3(h, 0, v));
    }
}
