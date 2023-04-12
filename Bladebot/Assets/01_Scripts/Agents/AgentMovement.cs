using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 8f, _gravity = -9.8f;

    private CharacterController _charController;
    private AgentAnimator _agentAnimator;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity;
    private float _verticalVelocity;

    public bool IsActiveMove { get; set; }

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
    }

    public void SetMovementVelocity(Vector3 value) // NormalState에서 넘어옴
    {
        _movementVelocity = value;
    }

    private void CalculatePlayerMovement()
    {
        _movementVelocity.Normalize();
        // _movementVelocity = _movementVelocity.normalized;

        _movementVelocity = Quaternion.Euler(0, -45f, 0) * _movementVelocity; // 로컬축을 기준으로 회전
        // 45f로 하면 월드축을 기준으로 회전함

        _agentAnimator?.SetSpeed(_movementVelocity.sqrMagnitude); //이동속도 반영

        _movementVelocity *= _moveSpeed * Time.fixedDeltaTime;
        if (_movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }
    }

    public void SetRotation(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
        _agentAnimator?.SetSpeed(0); 
    }

    private void FixedUpdate()
    {
        if (IsActiveMove) CalculatePlayerMovement();

        if (_charController.isGrounded == false)
        {
            _verticalVelocity = _gravity * Time.fixedDeltaTime;
        }
        else
        {
            _verticalVelocity = _gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
        _charController.Move(move);
        _agentAnimator?.SetAirbone(!_charController.isGrounded);
    }
}