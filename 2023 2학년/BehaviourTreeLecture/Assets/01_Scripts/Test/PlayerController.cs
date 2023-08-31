using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _gravity = -9.8f;

    private float _gravityMultiplier = 1f;
    [SerializeField] private float _jumpPower = 4f;

    private CharacterController _characterController;
    public bool IsGround => _characterController.isGrounded;

    private Vector2 _inputDirection;
    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;

    private bool _activeMove = true;
    public bool ActiveMove { get; set; }

    private PlayerInput _playerInput;

    [SerializeField] private Bullet bullet;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMovement += SetPlayerMovement;
        _playerInput.OnFire += FireBullet;
    }

    private void FireBullet()
    {
        Debug.Log("Fire");
    }

    private void SetPlayerMovement(Vector2 value)
    {
        _inputDirection = value;
    }

    private void CaculatePlayerMovement()
    {
        _movementVelocity = new Vector3(_inputDirection.x, 0, _inputDirection.y) * (_moveSpeed * Time.fixedDeltaTime);

        if (_movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);

            // 아래 코드는 forward 방향을 아래 value값으로 자동회전을 해준다.
            // transform.forward = _movementVelocity;
        }
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity < 0)
        {
            _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += _gravity * _gravityMultiplier * Time.fixedDeltaTime;
        }
        _movementVelocity.y = _verticalVelocity;
    }

    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
    }

    public void SetMovement(Vector3 value)
    {
        _movementVelocity = value;
        _movementVelocity.y = 0; // <- 입맛에 맞게 커스텀 하세요.
        _verticalVelocity = value.y;
    }

    private void Move()
    {
        _characterController.Move(_movementVelocity);
    }

    private void FixedUpdate()
    {
        if (_activeMove)
        {
            CaculatePlayerMovement();
        }
        ApplyGravity();
        Move();
    }
}
