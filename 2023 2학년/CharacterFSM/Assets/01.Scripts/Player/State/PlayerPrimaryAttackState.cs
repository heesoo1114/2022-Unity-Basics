using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _comboWindow = 2f;
    private readonly int _comboCountHash = Animator.StringToHash("ComboCounter");

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
        _comboWindow = _player.comboWindow;
    }

    public override void Enter()
    {
        base.Enter();
        //콤보를 제시간에 못넣었거나 3타까지 다 진행했거나.
        if(_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0;
        }

        _player.AnimatorCompo.SetInteger(_comboCountHash, _comboCounter);

        //_player.AnimatorCompo.speed = _player.attackSpeed;

        // 약간 앞으로 전진하면서 타격감을 느낄 수 있도록 
        // 사용자가 키를 누르는 방향대로 갈 수 있도록 해줘야 해.
        // 플레이어 setVelocity를 이용해서 값을 주고 
        // 0.2초정도 후에 StopImmediately를 호출
        float attackDirection = _player.FacingDirection;
        float xInput = _player.PlayerInput.XInput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            attackDirection = Mathf.Sign(xInput);
        }
        
        Vector2 currentMovemnt = _player.attackMovement[_comboCounter];
        _player.SetVelocity(currentMovemnt.x * attackDirection, currentMovemnt.y);
        _player.StartCoroutine(DelayStop(0.2f));
    }

    IEnumerator DelayStop(float time)
    {
        yield return new WaitForSeconds(time);
        _player.StopImmediately(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_triggerCalled) //애니메이션이 끝났는지 확인하고 끝났으면 idle로 전환
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        ++_comboCounter;
        _lastAttackTime = Time.time;
        base.Exit();
    }

}
