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
        //������ FacingDireciton �ݴ�������� ������ ���ָ� �ȴ�.
        _player.SetVelocity(-5f * _player.FacingDirection, _player.jumpForce);
        _player.StartCoroutine(DelayAir(0.4f));
    }

    public override void UpdateState()
    {
        base.UpdateState(); //0.4�ʵ� Ȱ���ϰ� �ٷ� Fall���·� ��ȯ�Ǹ� ��.
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
