using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        //점프를 FacingDireciton 반대방향으로 점프를 해주면 된다.
        _player.SetVelocity(-5f * _player.FacingDirection, _player.jumpForce);
        _player.StartCoroutine(DelayAir(0.4f));
    }

    public override void UpdateState()
    {
        base.UpdateState(); //0.4초도 활공하고 바로 Fall상태로 전환되면 돼.
    }

    public override void Exit()
    {
        base.Exit();
    }


    IEnumerator DelayAir(float time)
    {
        yield return new WaitForSeconds(time);
        _stateMachine.ChangeState(PlayerStateEnum.Fall);
    }
}
