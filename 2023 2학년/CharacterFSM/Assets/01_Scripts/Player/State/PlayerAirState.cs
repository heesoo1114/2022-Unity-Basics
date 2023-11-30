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

        // �տ� ���� �����Ǹ� WallSlide ���·� ��ȯ�Ǹ� ��
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
