using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
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

        if (Mathf.Abs(xInput) > 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
