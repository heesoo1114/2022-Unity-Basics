using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _comboWindow = 1f;
    private readonly int _comboCountHash = Animator.StringToHash("ComboCounter");

    private float _frontDir;

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
        _comboWindow = _player.comboWindow;
    }

    public override void Enter()
    {
        base.Enter();

        // 콤보를 제시간에 못 넣었거나 3타까지 다 진행했다면
        if (_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0;
        }

        _player.AnimatorCompo.SetInteger(_comboCountHash, _comboCounter);

        // 전진하면서 타격감을 주는 거 (0.2초 후에 StopImmediately)
        float xInput = _player.PlayerInput.XInput;
        float attackDir = _player.FacingDirection;
        bool isXKeyPress = Mathf.Abs(xInput) > 0.05f;

        if (isXKeyPress) // 입력이 이루어졌다면
        {
            attackDir = Mathf.Sign(xInput);
        }

        Vector2 currentMovement = _player.attackMovement[_comboCounter];
        _player.SetVelocity(currentMovement.x * attackDir, currentMovement.y);
        _player.StartCoroutine(DelayStop(0.2f));
    }

    private IEnumerator DelayStop(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _player.StopImmediately(false);
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
        _comboCounter++;
        _lastAttackTime = Time.time;
        base.Exit();
    }
    
}
