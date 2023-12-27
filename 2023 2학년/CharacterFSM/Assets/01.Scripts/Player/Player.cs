using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("충돌체크")]
    [SerializeField] protected Transform _groundChecker;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _whatIsGround;

    [SerializeField] private InputReader _inputReader;
    public InputReader PlayerInput => _inputReader;

    public float moveSpeed = 12f;
    public float jumpForce = 12f;
    public float dashDuration = 0.4f;
    public float dashSpeed = 20f;

    [Header("공격 셋팅")]
    public Vector2[] attackMovement;
    public float comboWindow = 0.5f;

    [Header("스텟")]
    [SerializeField] private CharacterStat _characterStat;
    public CharacterStat Stat => _characterStat;

    public Animator AnimatorCompo { get; private set; }
    public Rigidbody2D RigidbodyCompo { get; private set; }

    public PlayerStateMachine StateMachine { get; private set; }

    public int FacingDirection { get; private set; } = 1; //오른쪽이 1, 왼쪽 -1

    public SkillManager skill { get; private set; }


    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();

        StateMachine = new PlayerStateMachine();

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString(); //이걸 애니메이션 불 이름으로 재사용
            try
            {
                Type type = Type.GetType($"Player{typeName}State"); //이 클래스 타입이 불러와져
                PlayerState stateInstance = Activator.CreateInstance(type, this, StateMachine, typeName) as PlayerState;


                StateMachine.AddState(stateEnum, stateInstance); //이거 빼먹었었음.
            } catch (Exception e)
            {
                Debug.LogError($"{typeName} 클래스 인스턴스를 생성할 수 없습니다. {e.Message}");
            }
        }

        //처음시작할때 SO를 복제해서 넣어준다.
        _characterStat = Instantiate(_characterStat); //BT할때 배운거
        _characterStat.SetOwner(this);
    }

    private void Start()
    {
        skill = SkillManager.Instance;
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    #region 스킬 핸들링 로직들
    private void OnEnable()
    {
        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.CrystalSkillEvent += HandleCrystalSkillEvent;
    }

    private void OnDisable()
    {
        PlayerInput.DashEvent -= HandleDashEvent;
        PlayerInput.CrystalSkillEvent -= HandleCrystalSkillEvent;
    }


    private void HandleDashEvent()
    {
        // 스킬 매니저에서 대시를 가져와서
        // 그게 쿨이 아니면 실행하는거
        if(skill.GetSkill<DashSkill>().AttemptUseSkill())
        {
            StateMachine.ChangeState(PlayerStateEnum.Dash);
        }
    }

    private void HandleCrystalSkillEvent()
    {
        skill.GetSkill<CrystalSkill>()?.AttemptUseSkill();
    }

    #endregion

    public void SetVelocity(float x, float y, bool doNotFilp = false)
    {
        RigidbodyCompo.velocity = new Vector2(x, y);
        if (!doNotFilp)
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


        if(Keyboard.current.kKey.wasPressedThisFrame)
        {
            Stat.IncreaseStatBy(10, 3, StatType.strength);
        }

        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log($"현재 힘은 : {Stat.strength.GetValue()}");
        }
    }


    #region 플립컨트롤
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

    #region 체크 컬리전

    public virtual bool IsGroundDetected() =>
        Physics2D.Raycast(_groundChecker.position, Vector2.down,
                            _groundCheckDistance, _whatIsGround);

    public virtual bool IsWallDetected() =>
        Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection,
                            _wallCheckDistance, _whatIsGround);
    #endregion

    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
}
