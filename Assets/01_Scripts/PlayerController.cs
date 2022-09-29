using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float gravity = 20f;
    public float jumpSpeed = 15f;

    // public state
    public bool isJumping;

    private bool _startJump;
    private bool _releaseJump;

    private Vector2 _input;
    private Vector2 _moveDirection;
    private CharactorController2D _charactorController;

    private void Awake()
    {
        _charactorController = GetComponent<CharactorController2D>();
    }

    private void Update()
    {
        _moveDirection.x = _input.x * walkSpeed;
        
        // flipX 
        if(_moveDirection.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(_moveDirection.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (_charactorController.below) // on the ground
        {
            _moveDirection.y = 0;
            if(_startJump)
            {
                _startJump = false;
                _moveDirection.y = jumpSpeed;
                isJumping = true;
                _charactorController.DisableGroundCheck(0.1f);
            }
        }
        else // °øÁß¿¡
        {
            if(_releaseJump)
            {
                _releaseJump = false;
                if(_moveDirection.y > 0)
                {
                    _moveDirection.y *= 0.5f;
                }
            }
            GravityCalculation();
        }
        _charactorController.Move(_moveDirection * Time.deltaTime);
    }

    void GravityCalculation()
    {
        if(_moveDirection.y > 0f && _charactorController.above)
        {
            _moveDirection.y = 0f;
        }
        _moveDirection.y -= gravity * Time.deltaTime;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _startJump = true;
            _releaseJump = false;
        }
        else if(context.canceled)
        {
            _startJump = false;
            _releaseJump = true;
        }
    }
}
