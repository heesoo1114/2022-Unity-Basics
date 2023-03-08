using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 8f, _gravity = -9.8f;

    private CharacterController _characterController;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMoveInput();
    }

    private void UpdateMoveInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _movementVelocity = new Vector3(h, 0, v);
    }

    private void CalculatePlayerMovement()
    {
        // vector3는 stack
        _movementVelocity.Normalize(); // _movementVelocity의 값이 직접 변함
        _movementVelocity = Quaternion.Euler(0, -45f, 0) * _movementVelocity;

        _movementVelocity *= _moveSpeed * Time.fixedDeltaTime;
        if (_movementVelocity.sqrMagnitude > 0 )
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }
    }

    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement();

        if (_characterController.isGrounded == false)
        {
            _verticalVelocity = _gravity * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity = _gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
        _characterController.Move(move);
    }
}
