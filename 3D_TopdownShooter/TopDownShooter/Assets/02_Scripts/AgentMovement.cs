using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentMovement : MonoBehaviour
{
    [SerializeField]
    private MovementDataSO _movementSO;

    private Rigidbody2D _rigidbody;

    protected float _currentVelocity = 0;
    protected Vector2 _movementDirection;

    public UnityEvent<float> OnVelocityChange;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 moveInput)
    {
        // _movementDirection = moveInput.normalized * 5f;

        // 키가 눌렸을 때
        if(moveInput.sqrMagnitude > 0)
        {
            // 벡터 내적, 외적
            if(Vector2.Dot(moveInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = moveInput.normalized;
        }
        _currentVelocity = CalculateSpeed(moveInput);
    }

    private float CalculateSpeed(Vector2 moveInput)
    {
        if(moveInput.sqrMagnitude > 0)
        {
            _currentVelocity += _movementSO.acceleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _movementSO.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, _movementSO.maxSpeed);
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(_currentVelocity);
        _rigidbody.velocity = _movementDirection * _currentVelocity;
    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigidbody.velocity = Vector2.zero;
    }

}
