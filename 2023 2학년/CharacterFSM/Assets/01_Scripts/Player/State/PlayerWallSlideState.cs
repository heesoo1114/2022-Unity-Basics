using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // ������ ���ؼ� ��� �� �־�� �ϰ�
        _player.PlayerInput.JumpEvent += HandleJumpEvnet;
    }

    private void HandleJumpEvnet()
    {
        _stateMachine.ChangeState(PlayerStateEnum.WallJump);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float xInput = _player.PlayerInput.XInput;
        float yInput = _player.PlayerInput.YInput;

        if (yInput < 0)
        {
            _player.SetVelocity(0, _rigidBody.velocity.y, true);
        }
        else
        {
            _player.SetVelocity(0, _rigidBody.velocity.y * 0.7f, true);
        }

        // xinput�� �Է��� �Ǿ���, ���� ���� ���� ����(FacindDir)�� �ٸ��� �ԷµǾ��ٸ�
        // => Idle ���·� ��ȯ (�ڵ����� Idle���� Fall�� ��ȯ)
        if (Mathf.Abs(_player.FacingDirection + xInput) < 0.2f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        // ���� Ÿ�� �������ٰ� �׶��� �����Ǹ�
        // => Idle ���·� ��ȯ
        if (_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvnet;
        base.Exit();
    }
    
}
