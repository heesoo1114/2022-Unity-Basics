using System;

public abstract class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
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
        if (MathF.Abs(xInput) > 0.05f)
        {
            _player.SetVelocity(xInput * _player.moveSpeed * 0.8f, _rigidBody.velocity.y);
        }

        // 앞에 벽이 감지되면 WallSlide 상태로 변환되면 돼
        if (_player.IsWallDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.WallSlide);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
