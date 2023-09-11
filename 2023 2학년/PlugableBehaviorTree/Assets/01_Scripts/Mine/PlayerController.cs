using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerTransition _playerTransition;

    private PlayerState playetState;

    private void Awake()
    {
        _playerTransition = GetComponent<PlayerTransition>();
    }

    private void Start()
    {
        playetState = PlayerState.Idle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _playerTransition.ChangeState(ref playetState, PlayerState.Move);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _playerTransition.ChangeState(ref playetState, PlayerState.Idle);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerTransition.ChangeState(ref playetState, PlayerState.Jump);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _playerTransition.ChangeState(ref playetState, PlayerState.Attack, PlayerState.Move);
        }
    }
}
