using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocity(_rigidbody.velocity.x, _player.jumpForce, doNotFilp: true);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //속도가 -값으로 떨어질때 Fall상태로 전환되면 돼.
        if(_rigidbody.velocity.y < 0)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
