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

        // �޺��� ���ð��� �� �־��ų� 3Ÿ���� �� �����ߴٸ�
        if (_comboCounter > 2 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0;
        }

        _player.AnimatorCompo.SetInteger(_comboCountHash, _comboCounter);

        // �����ϸ鼭 Ÿ�ݰ��� �ִ� �� (0.2�� �Ŀ� StopImmediately)
        float xInput = _player.PlayerInput.XInput;
        float attackDir = _player.FacingDirection;
        bool isXKeyPress = Mathf.Abs(xInput) > 0.05f;

        if (isXKeyPress) // �Է��� �̷�����ٸ�
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
