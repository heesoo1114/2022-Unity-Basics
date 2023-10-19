using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("참조변수")]
    [SerializeField] private InputReader _inputReader;
    private PlayerAnimation _playerAnimation;

    [Header("셋팅값")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _aimingMoveSpeed = 3f;
    [SerializeField] private float _gravity = -9.8f;

    private float _currentSpeed;



    private float _gravityMultiplier = 1f;

    private CharacterController _characterController;
    public bool IsGround
    {
        get => _characterController.isGrounded;
    }

    private Vector2 _inputDirection;
    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;
    private Quaternion _degreeY45;

    //키보드로 움직이는 상태인가?
    private bool _activeMove = true;
    public bool ActiveMove
    {
        get => _activeMove;
        set => _activeMove = value;
    }

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _degreeY45 = Quaternion.AngleAxis(45f, Vector3.up);
        _characterController = GetComponent<CharacterController>();
        _inputReader.MovementEvent += SetMovement;
        _inputReader.ArmedEvent += OnHandleArm;
        _currentSpeed = _moveSpeed;
    }

    private void OnDestroy()
    {
        _inputReader.MovementEvent -= SetMovement;
        _inputReader.ArmedEvent -= OnHandleArm;
    }

    private void OnHandleArm(bool value)
    {
        if (value)
        {
            _currentSpeed = _aimingMoveSpeed;
            // _playerAnimation.SetMovementAnimatorSpeed(1f);
        }
        else
        {
            _currentSpeed = _moveSpeed;
            // _playerAnimation.SetMovementAnimatorSpeed(2f);
        }
    }

    //요것은 PlayInput에서 구독처리 될것임.
    public void SetMovement(Vector2 value)
    {
        _inputDirection = value;
        _playerAnimation.SetMove(_inputDirection.magnitude > 0.1f);
    }

    private void CalculatePlayerMovement()
    {
        Vector3 move = _degreeY45 * new Vector3(_inputDirection.x, 0, _inputDirection.y);
        _movementVelocity = move * (_currentSpeed * Time.deltaTime);
    }


    //즉시 정지
    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
    }

    //만약 다른 스크립트에서 이동을 건드리려 한다면 사용
    public void SetMovement(Vector3 value)
    {
        _movementVelocity = new Vector3(value.x, 0, value.z);
        _verticalVelocity = value.y;
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity < 0)  //땅에 착지 상태
        {
            _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += _gravity * _gravityMultiplier * Time.fixedDeltaTime;
        }

        _movementVelocity.y = _verticalVelocity;
    }

    private void Move()
    {
        _characterController.Move(_movementVelocity);
    }

    private void FixedUpdate()
    {
        //키보드로 움직일때만 이렇게 움직이고
        if (_activeMove)
        {
            CalculatePlayerMovement();
        }
        ApplyGravity(); //중력 적용
        Move();
    }
}