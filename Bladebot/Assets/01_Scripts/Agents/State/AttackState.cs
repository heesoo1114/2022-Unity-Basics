using System;
using UnityEngine;
using Core;

public class AttackState : CommonState
{
    public event Action<int> OnAttackStart = null; // 공격시 콤보값을 알려주는 이벤트
    public event Action OnAttackstateEnd = null; // 공격상태를 나갈 때 발행하는 이벤트

    [SerializeField]
    private float _keyDelay = 0.5f; // 0.5초 내에 마우스를 한 번 눌러줘야 실행

    private int _currentCombo = 0; // 현재 콤보 수치
    private bool _canAttack = true; // 현재 공격이 가능한지
    private float _keyTimer = 0;

    private float _attackStartTime; // 공격이 시작된 시간
    [SerializeField]
    private float _attackSlideDuration = 0.2f, _attackSlideSpeed = 0.1f;

    private DamageCaster _dameCaster;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _dameCaster = agentRoot.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    private void OnDamageCastHandle()
    {
        _dameCaster.CastDamage();
    }

    public override void OnEnterState()
    {
        _agentInput.OnAttackKeyPress += OnAttackHandle;
        _agentAnimator.OnAnimationEndTrigger += OnAnimationHandle;
        _agentInput.OnRollingKeyPress += OnRollingHandle;
        _agentAnimator.OnAnimationEventTrigger += OnDamageCastHandle;

        _currentCombo = 0;
        _canAttack = true;
        _agentAnimator.SetAttackState(true); // 공격 상태로 전환
        OnAttackHandle(); // 수동으로 공격 시작
    }

    public override void OnExitState()
    {
        _agentInput.OnAttackKeyPress -= OnAttackHandle;
        _agentAnimator.OnAnimationEndTrigger -= OnAnimationHandle;
        _agentInput.OnRollingKeyPress -= OnRollingHandle;
        _agentAnimator.OnAnimationEventTrigger -= OnDamageCastHandle;
        _agentAnimator.SetAttackState(false); // 공격상태로 전환
        _agentAnimator.SetAttackTrigger(false);
        OnAttackstateEnd?.Invoke();
    }

    private void OnRollingHandle()
    {
        _agentController.ChangeState(StateType.Rolling);
    }

    private void OnAnimationHandle()
    {
        _canAttack = true;
        _keyTimer = _keyDelay; // 0.5초 시간 넣어주고 이제부터 카운트 시작
    }

    private void OnAttackHandle()
    {
        if (_canAttack && _currentCombo < 3)
        {
            _attackStartTime = Time.time; // 공격 시작 기간을 기록
            _canAttack = false;
            _agentAnimator.SetAttackTrigger(true);
            _currentCombo++;
            OnAttackStart?.Invoke(_currentCombo);
        }
    }

    public override void UpdateState()
    {
        if (_canAttack && _keyTimer > 0)
        {
            _keyTimer -= Time.deltaTime;
            if (_keyTimer <= 0)
            {
                _agentController.ChangeState(StateType.Normal);
            }
        }

        // 슬라이딩이 되어야 하는 시간이라는 뜻
        if (Time.time < _attackStartTime + _attackSlideDuration)
        {
            float timePassed = Time.time - _attackStartTime; // 공격 시작한지 몇 초
            float lerpTime = timePassed / _attackSlideDuration; // 0 ~ 1 값으로 변환

            _agentMovement.SetMovementVelocity(Vector3.Lerp(_agentMovement.transform.forward * _attackSlideSpeed, Vector3.zero, lerpTime));
        }
    }
}
