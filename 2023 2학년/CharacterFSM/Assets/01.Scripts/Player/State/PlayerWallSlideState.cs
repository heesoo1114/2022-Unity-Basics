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
        //������ ���ؼ� ��� �� �־�� �ϰ�.
        _player.PlayerInput.JumpEvent += HandleJumpEvent;
    }

    private void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerStateEnum.WallJump);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float xInput = _player.PlayerInput.XInput;
        float yInput = _player.PlayerInput.YInput;

        if(yInput < 0)
        {
            _player.SetVelocity(0, _rigidbody.velocity.y, true);
        }
        else
        {
            _player.SetVelocity(0, _rigidbody.velocity.y * 0.7f, true);
        }

        //xInput�� �Է��� �Ǿ���, ���� �������� ����(FacingDireciton)�� �ٸ��� �ԷµǾ��ٸ�
        // -> Idle���·� ��ȯ�ؾ�
        if(Mathf.Abs(_player.FacingDirection + xInput) < 0.2f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        //���� Ÿ�� �������ٰ� �׶��尡 �����Ǿ��ٸ�
        // -> idle���� ��ȯ�ؾ߰���.
        if(_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }

}
