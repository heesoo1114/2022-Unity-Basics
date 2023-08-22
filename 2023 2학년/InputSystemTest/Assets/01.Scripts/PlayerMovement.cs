using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody _rigidBody;

    private Vector2 _inputDir;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    [SerializeField] private bool isGround;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody>();

        _playerInput.OnJump += Jump;
        _playerInput.OnMovement += SetMovementValue;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        if (!isGround) return;

        _rigidBody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        isGround = false;
    }

    private void SetMovementValue(Vector2 value)
    {
        _inputDir = value;
    }

    private void Move()
    {
        Vector3 targetTr = new Vector3(_inputDir.x, 0, _inputDir.y);
        _rigidBody.AddForce(targetTr * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
    }
}
