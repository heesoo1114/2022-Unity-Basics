using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private ShootingSystem _shootingSystem;

    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpPower = 3f;
    [SerializeField] private float _desiredRotationSpeed = 0.3f;
    [SerializeField] private float _allowPlayerRotation = 0.1f;
    [SerializeField] private Transform _modelTrm;
    [SerializeField] private PlayerAnimator _animator;

    private CharacterController _characterController;
    public bool IsGround => _characterController.isGrounded;


    private float _verticalVelocity;
    private Vector3 _movementVelocity;
    private Vector2 _inputDirection;
    private Vector3 _desiredMoveDirection;
    private Camera _mainCamera;
    public bool blockRotationPlayer = false; //사격모드일때는 플레이어가 회전하지 않도록 한다.

    private void Awake()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _shootingSystem = GetComponent<ShootingSystem>();
        _inputReader.MovementEvent += SetMovement;
        _inputReader.JumpEvent += Jump;
    }

    private void OnDestroy()
    {
        _inputReader.MovementEvent -= SetMovement;
        _inputReader.JumpEvent -= Jump;
    }

    private void SetMovement(Vector2 value)
    {
        _inputDirection = value;
    }

    private void Jump()
    {
        if (!IsGround) return;
        _verticalVelocity += _jumpPower;
    }


    private void CalculatePlayerMovement()
    {
        var forward = _mainCamera.transform.forward;
        var right = _mainCamera.transform.right;
        forward.y = 0;
        right.y = 0;

        _desiredMoveDirection = forward.normalized * _inputDirection.y + right.normalized * _inputDirection.x;

        if (blockRotationPlayer == false && _inputDirection.sqrMagnitude > _allowPlayerRotation)
        {
            //발사중이 아니고 움직이고 있다면 
            _modelTrm.rotation = Quaternion.Slerp(
                _modelTrm.rotation,
                Quaternion.LookRotation(_desiredMoveDirection),
                _desiredRotationSpeed);
        }

        _movementVelocity = _desiredMoveDirection * (_moveSpeed * Time.fixedDeltaTime);
    }

    public void RotateToCamera(Transform t)
    {
        var forward = _mainCamera.transform.forward;
        var rot = _modelTrm.rotation;
        _desiredMoveDirection = forward;
        Quaternion lookAtRotation = Quaternion.LookRotation(_desiredMoveDirection);
        Quaternion lookAtRotationOnlyY = Quaternion.Euler(
            rot.eulerAngles.x,
            lookAtRotation.eulerAngles.y,
            rot.eulerAngles.z);
        t.rotation = Quaternion.Slerp(rot, lookAtRotationOnlyY, _desiredRotationSpeed);
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity < 0) //땅에 착지되어있는 상태
        {
            _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += _gravity * Time.fixedDeltaTime;
        }

        _movementVelocity.y = _verticalVelocity;
    }

    private void Move()
    {
        _characterController.Move(_movementVelocity);
    }

    public void ApplyAnimation()
    {
        _animator.SetShooting(blockRotationPlayer); //블록상태면 슈팅이 된거니까 전환

        _animator.SetMoving(_inputDirection != Vector2.zero);
        _animator.SetShooting(_shootingSystem._isPressFire);

        float speed = _inputDirection.sqrMagnitude;
        _animator.SetBlendValue(speed);
        _animator.SetXY(_inputDirection);
    }

    private void FixedUpdate()
    {
        ApplyAnimation(); //애니메이션 적용해주고
        CalculatePlayerMovement(); //이동 방향 계산해주고
        ApplyGravity(); //중력 계산해주고
        Move(); //전부 완료되었다면 실제 이동
    }

}