using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    protected float _currentVelocity = 0;
    protected Vector2 _movementDirection;

    [SerializeField]
    private AgentMoveSO _movementSO;

    protected void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            if (Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalcSpeed(movementInput);
    }
    private float CalcSpeed(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _movementSO.accel * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _movementSO.deAccel * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity, 0, _movementSO.maxSpeed);
    }

    private void FixedUpdate()
    {
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void StopImmediatley()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }
}
