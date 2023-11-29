using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _comboWindow = 2f;
    private readonly int _comboCountHash = Animator.StringToHash("ComboCounter");

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 콤보를 제시간에 못 넣었거나 3타까지 다 진행했거나
        if (_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0;
        }

        _player.AnimatorCompo.SetInteger(_comboCountHash, _comboCounter);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
