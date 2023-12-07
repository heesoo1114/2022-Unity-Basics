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
        //������ �� �ƹ�Ű�� �ȴ����ִٸ� �ٶ󺸴� �������� ��ø� �Ҳ���
        //Ű�� ���� ���¶�� ���� Ű �������� ��ø� �Ҳ���
        float xInput = _player.PlayerInput.XInput;
        _dashDirection = Mathf.Abs(xInput) >= 0.05f ? Mathf.Sign(xInput) : _player.FacingDirection;
        //���� �ð��� ���� �ð��� �����ְ�
        _dashStartTime = Time.time;

        CloneSkill cloneSkill = _player.skill.GetSkill<CloneSkill>();
        cloneSkill.CreateClone(_player.transform, Vector3.zero);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // ����� �������� �÷��̾ ��� ���ǵ� ��ŭ �̵��Ҳ���
        _player.SetVelocity(_player.dashSpeed * _dashDirection, 0);

        //��� ���۽ð��� ���ؼ� ���� ���� �ð��� dashDuration�� �����ٸ�
        // Idle���·� �Ѱ��ָ� �ȴ�.
        if(_dashStartTime + _player.dashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        //���� ��ų�� �����Ϸ��� ��� ������������ ���� ��ų �����Ű��
        base.Exit();
    }

}
