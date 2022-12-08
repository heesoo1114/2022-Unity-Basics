using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Animator _anim;
    [SerializeField] AnimationCurve _animCurve;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float accelerationMaxTime = 1f;
    private float buttonHoldTime;
    private bool isMoving;


    public float moveSpeed = 5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveVelocity = new Vector2(x, y).normalized * moveSpeed;

        moveSpeed = CalulateSpeed(moveVelocity, _animCurve);

        _rigidbody.velocity = moveVelocity;

        AnimatorSet(moveVelocity);
    }

    private void AnimatorSet(Vector2 moveVelocity)
    {
        _anim.SetFloat("InputX", moveVelocity.x);
        _anim.SetFloat("InputY", moveVelocity.y);
    }

    private void SetAccelerationParamenters(Vector2 input)
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
            float acceleration = _animCurve.Evaluate(buttonHoldTime / accelerationMaxTime);
            return maxSpeed * acceleration;
        }
        else return 0;
    }
}
