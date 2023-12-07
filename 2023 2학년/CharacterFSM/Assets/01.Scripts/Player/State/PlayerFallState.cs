using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //땅을 감지했다면 바로 Idle상태로 넘어가야 한다.
        if(_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
