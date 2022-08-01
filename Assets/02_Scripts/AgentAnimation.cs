using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    protected Animator _animator;

    protected readonly int _walkHash = Animator.StringToHash("Walk");
    protected readonly int _deadHash = Animator.StringToHash("Death");

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkAnimation(bool value)
    {
        _animator.SetBool(_walkHash, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }
}
