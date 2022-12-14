using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3;

    private Rigidbody2D _rigid;
    private Animator _animator;
    [SerializeField]
    AnimationCurve _animationCurve;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] float accelerationMaxTime = 1f;
    float buttonHoldTime;
    bool isMoving;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        SetAccelationParamenters(dir);
        speed = CalulateSpeed(dir, _animationCurve);

        _rigid.velocity = dir.normalized * speed;

        AnimatorSet(dir);
    }

    private void AnimatorSet(Vector2 dir)
    {
        _animator.SetFloat("InputX", dir.x);
        _animator.SetFloat("InputY", dir.y);
    }

    void SetAccelationParamenters(Vector2 input)
    {
        if (input.magnitude > 0)
        {
            isMoving = true;
            buttonHoldTime += Time.deltaTime;
        }
        else
        {
            isMoving = false;
            buttonHoldTime = 0;
        }
    }

    float CalulateSpeed(Vector2 input, AnimationCurve anmationCurve)
    {
        if (isMoving)
        {
            float acceleration = _animationCurve.Evaluate(buttonHoldTime / accelerationMaxTime);
            return maxSpeed * acceleration;
        }
        else return 0;
    }
}
