using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;
    protected Rigidbody2D _rigidBody;

    protected int _animBoolHash;

    protected bool _triggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animationBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
        _rigidBody = player.RigidbodyCompo;
    }

    // ���¿� ������ �� ���� ��
    public virtual void Enter()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, true);
        _triggerCalled = false;
    }

    // ���¿� �ִ� ���� ����� �� ��
    public virtual void UpdateState()
    {

    }
    
    // ���¸� ���� �� ���� ��
    public virtual void Exit()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, false);
        _triggerCalled = true;
    }

    // animation event
    public virtual void AnimationFinishTrigger()
    {
        _triggerCalled = true;
    }

}
