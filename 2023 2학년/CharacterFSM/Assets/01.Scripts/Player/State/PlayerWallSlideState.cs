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
        //점프를 통해서 벗어날 수 있어야 하고.
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

        //xInput이 입력이 되었고, 현재 진행중인 방향(FacingDireciton)과 다르게 입력되었다면
        // -> Idle상태로 전환해야
        if(Mathf.Abs(_player.FacingDirection + xInput) < 0.2f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        //벽을 타고 내려가다가 그라운드가 감지되었다면
        // -> idle상태 전환해야겠지.
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
