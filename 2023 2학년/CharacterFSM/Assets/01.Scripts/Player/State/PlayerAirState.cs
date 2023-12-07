using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirState : PlayerState
{
    protected PlayerAirState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        float xInput = _player.PlayerInput.XInput;
        if(Mathf.Abs( xInput) > 0.05f )
        {
            _player.SetVelocity(xInput * _player.moveSpeed * 0.8f, _rigidbody.velocity.y);
        }

        //�տ� ���� �����Ǹ� WallSlide���·� ��ȯ�Ǹ� �ȴ�.
        if(_player.IsWallDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.WallSlide);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
