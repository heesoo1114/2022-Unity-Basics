using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private readonly int _moveXHash = Animator.StringToHash("move_x");
    private readonly int _moveYHash = Animator.StringToHash("move_y");
    private readonly int _isMoveHash = Animator.StringToHash("is_move");

    private Animator _rigAnimator;
    private readonly int _armedHash = Animator.StringToHash("is_armed");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigAnimator = transform.Find("RigBuilder").GetComponent<Animator>();   
    }

    private void Start()
    {
        _rigAnimator.enabled = true;
    }

    public void SetArmed(bool value)
    {
        _rigAnimator.SetBool(_armedHash, value);
    }

    public void SetAnimationDirection(Vector2 dir)
    {
        _animator.SetFloat(_moveXHash, dir.x);
        _animator.SetFloat(_moveYHash, dir.y);
    }

    public void SetMove(bool value)
    {
        _animator.SetBool(_isMoveHash, value);
    }

}