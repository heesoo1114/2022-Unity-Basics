using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float _dashStartTime;
    private float _dashEndTime;

    private float _dashDir;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 키가 안 눌린 상태라면 바라보는 방향으로 대시
        // 키가 눌린 상태라면 눌린 키 방향으로 대시
        float xInput = _player.PlayerInput.XInput;
        _dashDir = Mathf.Abs(xInput) >= 0.05f ? Mathf.Sign(xInput) : _player.FacingDirection;

        // 시작 시간에 현재 시간을 더해주고
        _dashStartTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // 대시의 방향으로 플레이어를 대시 스피드 만큼 이동
        // _player.dashSpeed
        _player.SetVelocity(_dashDir * _player.dashSpeed, 0);

        // 시작하고 나서 대쉬가 끝났는지 확인하고
        // 다시 Idle 상태로 넘겨주기
        if (_dashStartTime + _player.dashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
