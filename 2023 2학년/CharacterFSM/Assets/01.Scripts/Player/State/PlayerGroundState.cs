using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundState : PlayerState
{
    protected PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.JumpEvent += HandleJumpEvent;
        _player.PlayerInput.PrimaryAttackEvent += HandlePrimaryAttackEvent;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvent;
        _player.PlayerInput.PrimaryAttackEvent -= HandlePrimaryAttackEvent;
        base.Exit();
    }

    //키가 눌렸을 때 공격 상태로 전환만 하면 된다.
    private void HandlePrimaryAttackEvent()
    {
        if (_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.PrimaryAttack);
        }
    }

    protected void HandleJumpEvent()
    {
        if(_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }
}
