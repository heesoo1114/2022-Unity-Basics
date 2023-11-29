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
        // 점프를 통해서 벗어날 수 있어야 하고
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

        // xinput이 입력이 되었고, 현재 진행 중인 방향(FacindDir)과 다르게 입력되었다면
        // => Idle 상태로 전환 (자동으로 Idle에서 Fall로 전환)
        if (Mathf.Abs(_player.FacingDirection + xInput) < 0.2f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        // 벽을 타고 내려가다가 그라운드 감지되면
        // => Idle 상태로 전환
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
