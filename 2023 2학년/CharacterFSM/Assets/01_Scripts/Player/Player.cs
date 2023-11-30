using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public InputReader PlayerInput => _inputReader;

    [Header("Collision Check")]
    [SerializeField] protected Transform _groundChecker;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _whatIsGround;

    [Header("MoveValue")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    public float dashDuration = 0.4f;
    public float dashSpeed = 20f;

    [Header("AttackSetting")]
    public Vector2[] attackMovement;
    public float comboWindow = 0.5f;

    [Header("Stat")]
    [SerializeField] private CharacterStat _characterStat;
    public CharacterStat Stat => _characterStat;

    public Animator AnimatorCompo { get; private set; }
    public Rigidbody2D RigidbodyCompo { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public int FacingDirection { get; private set; } = 1; // �������� 1, ���� -1


    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();

        StateMachine = new PlayerStateMachine();

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString(); // �ִϸ��̼� �� �̸����� ����
            try
            {
                Type type = Type.GetType($"Player{typeName}State"); // Ŭ���� Ÿ���� �ҷ�����
                PlayerState stateInstance = Activator.CreateInstance(type, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, stateInstance); // ��ųʸ��� ������Ʈ �߰�
            }
            catch (Exception e)
            {
                Debug.LogError($"{typeName} Ŭ���� �ν��Ͻ��� ������ �� �����ϴ�. {e.Message}");
            }
        }

        _characterStat = Instantiate(_characterStat);
        _characterStat.SetOwner(this);
    }

    private void OnEnable()
    {
        PlayerInput.DashEvent += HandleDashEvent;
    }

    private void OnDisable()
    {
        PlayerInput.DashEvent -= HandleDashEvent;
    }

    // ��� �Է� �ڵ鸵
    private void HandleDashEvent()
    {
        StateMachine.ChangeState(PlayerStateEnum.Dash);
    }

    private void Start()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    public void SetVelocity(float x, float y, bool doNotFlip = false)
    {
        RigidbodyCompo.velocity = new Vector2(x, y);
        if (!doNotFlip)
        {
            FlipController(x);
        }
    }

    public void StopImmediately(bool withYAxis)
    {
        if (withYAxis)
        {
            RigidbodyCompo.velocity = Vector2.zero;
        }
        else
        {
            RigidbodyCompo.velocity = new Vector2(0, RigidbodyCompo.velocity.y);
        }
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.UpdateState();

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Stat.IncreaseStatBy(10, 3, StatType.strength);
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log(Stat.strength.GetValue());
        }
    }

    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    #region �ø���Ʈ��

    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1;
        transform.Rotate(0, 180f, 0);
    }

    public virtual void FlipController(float x)
    {
        bool gotoRight = x > 0 && FacingDirection < 0;
        bool gotoLeft = x < 0 && FacingDirection > 0;

        if (gotoRight || gotoLeft)
        {
            Flip();
        }
    }

    #endregion

    #region üũ �ø���

    public virtual bool IsGroundDetected() => Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection, _wallCheckDistance, _whatIsGround);
    
    #endregion

}