using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;
    protected Rigidbody2D _rigidBody;

    protected int _animBoolHash;
    protected readonly int _yVelocityHash = Animator.StringToHash("y_velocity");

    protected bool _triggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animationBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
        _rigidBody = player.RigidbodyCompo;
    }

    // 상태에 들어왔을 때 해줄 일
    public virtual void Enter()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, true);
        _triggerCalled = false;
    }

    // 상태에 있는 동안 해줘야 할 일
    public virtual void UpdateState()
    {
        _player.AnimatorCompo.SetFloat(_yVelocityHash, _rigidBody.velocity.y);
    }
    
    // 상태를 나갈 때 해줄 일
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
