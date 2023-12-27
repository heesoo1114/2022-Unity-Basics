using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float _dashStartTime;
    private float _dashDirection;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //들어왔을 때 아무키도 안눌려있다면 바라보는 방향으로 대시를 할꺼고
        //키가 눌린 상태라면 눌린 키 방향으로 대시를 할꺼야
        float xInput = _player.PlayerInput.XInput;
        _dashDirection = Mathf.Abs(xInput) >= 0.05f ? Mathf.Sign(xInput) : _player.FacingDirection;
        //시작 시간에 현재 시간을 더해주고
        _dashStartTime = Time.time;

        _player.skill.GetSkill<CloneSkill>()?.CreateCloneOnDashStart(_player.transform, Vector3.zero);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // 대시의 방향으로 플레이어를 대시 스피드 만큼 이동할꺼야
        _player.SetVelocity(_player.dashSpeed * _dashDirection, 0);

        //대시 시작시간에 비해서 지금 현재 시간이 dashDuration이 끝났다면
        // Idle상태로 넘겨주면 된다.
        if(_dashStartTime + _player.dashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        //만약 스킬과 연계할꺼면 대시 종료지점에서 뭔가 스킬 연계시켜
        _player.skill.GetSkill<CloneSkill>()?.CreateCloneOnDashOver(_player.transform, Vector3.zero);
        base.Exit();
    }

}
