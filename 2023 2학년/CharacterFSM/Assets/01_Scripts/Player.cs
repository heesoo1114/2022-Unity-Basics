using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public InputReader PlayerInput => _inputReader;

    [Header("MoveValue")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    public float dashDuration = 0.4f;
    public float dashSpeed = 20f;

    public Animator AnimatorCompo { get; private set; }
    public Rigidbody2D RigidbodyCompo { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();

        StateMachine = new PlayerStateMachine();

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString(); // 애니메이션 불 이름으로 재사용
            try
            {
                Type type = Type.GetType($"Player{typeName}State"); // 클래스 타입이 불러와짐
                PlayerState stateInstance = Activator.CreateInstance(type, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, stateInstance); // 딕셔너리에 스테이트 추가
            }
            catch (Exception e)
            {
                Debug.LogError($"{typeName} 클래스 인스턴스를 생성할 수 없습니다. {e.Message}");
            }
        }
    }

    private void Start()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    public void SetVelocity(float x, float y, bool doNotFlip = false)
    {
        RigidbodyCompo.velocity = new Vector2(x, y);
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }
}