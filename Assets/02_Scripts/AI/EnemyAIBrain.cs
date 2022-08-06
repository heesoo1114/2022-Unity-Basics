using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBrain : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;
    public UnityEvent OnFireButtonPress;
    public UnityEvent OnFireButtonRelease;

    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private Transform _target; // 나중에 게임매니저를 통해서 가져 올 예정, 지금은 걍 드래그드랍
    public Transform Target
    {
        get => _target;
    }

    private AIActionData _aiActionData;
    public AIActionData AIActionData { get => _aiActionData; }
    private AIMovementData _aiMovementData;
    public AIMovementData AIMovementData { get => _aiMovementData; }

    private Transform _basePosition;
    public Transform BasePosition { get => _basePosition; }

    private Enemy _enemy;
    public Enemy Enemy => _enemy;

    protected virtual void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        _aiMovementData = transform.Find("AI").GetComponent<AIMovementData>();
        _basePosition = transform.Find("BasePosition");
        _enemy = GetComponent<Enemy>();
        _target = GameObject.Find("Player").GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        _target = GameManager.Instance.Player;
    }

    protected void Update()
    {
        if(_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
            return;
        }
        else
        {
            _currentState.UpDateState();
        }
    }


    public void ChangeState(AIState state)
    {
        _currentState = state;
    }

    public virtual void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void Move(Vector2 direction, Vector2 targetPos)
    {
        OnMovementKeyPress?.Invoke(direction);
        OnPointerPositionChanged?.Invoke(targetPos);
    }
}
