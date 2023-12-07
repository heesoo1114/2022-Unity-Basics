using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _comboWindow = 2f;
    private readonly int _comboCountHash = Animator.StringToHash("ComboCounter");

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
        _comboWindow = _player.comboWindow;
    }

    public override void Enter()
    {
        base.Enter();
        //�޺��� ���ð��� ���־��ų� 3Ÿ���� �� �����߰ų�.
        if(_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0;
        }

        _player.AnimatorCompo.SetInteger(_comboCountHash, _comboCounter);

        //_player.AnimatorCompo.speed = _player.attackSpeed;

        // �ణ ������ �����ϸ鼭 Ÿ�ݰ��� ���� �� �ֵ��� 
        // ����ڰ� Ű�� ������ ������ �� �� �ֵ��� ����� ��.
        // �÷��̾� setVelocity�� �̿��ؼ� ���� �ְ� 
        // 0.2������ �Ŀ� StopImmediately�� ȣ��
        float attackDirection = _player.FacingDirection;
        float xInput = _player.PlayerInput.XInput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            attackDirection = Mathf.Sign(xInput);
        }
        
        Vector2 currentMovemnt = _player.attackMovement[_comboCounter];
        _player.SetVelocity(currentMovemnt.x * attackDirection, currentMovemnt.y);
        _player.StartCoroutine(DelayStop(0.2f));
    }

    IEnumerator DelayStop(float time)
    {
        yield return new WaitForSeconds(time);
        _player.StopImmediately(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_triggerCalled) //�ִϸ��̼��� �������� Ȯ���ϰ� �������� idle�� ��ȯ
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        ++_comboCounter;
        _lastAttackTime = Time.time;
        base.Exit();
    }

}
