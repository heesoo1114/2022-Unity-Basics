using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float _dashStartTime;
    private float _dashEndTime;

    private float _dashDir;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Ű�� �� ���� ���¶�� �ٶ󺸴� �������� ���
        // Ű�� ���� ���¶�� ���� Ű �������� ���
        float xInput = _player.PlayerInput.XInput;
        _dashDir = Mathf.Abs(xInput) >= 0.05f ? Mathf.Sign(xInput) : _player.FacingDirection;

        // ���� �ð��� ���� �ð��� �����ְ�
        _dashStartTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // ����� �������� �÷��̾ ��� ���ǵ� ��ŭ �̵�
        // _player.dashSpeed
        _player.SetVelocity(_dashDir * _player.dashSpeed, 0);

        // �����ϰ� ���� �뽬�� �������� Ȯ���ϰ�
        // �ٽ� Idle ���·� �Ѱ��ֱ�
        if (_dashStartTime + _player.dashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
