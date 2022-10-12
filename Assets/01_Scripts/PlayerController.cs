using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // player properties
    [Header("Player Properties")]
    public float walkSpeed = 10f;
    public float creepSpeed = 5f;
    public float gravity = 20f;
    public float jumpSpeed = 15f;
    public float doubleJumpSpeed = 10f;
    public float xWallJumpSpeed = 15f;
    public float yWallJumpSpeed = 15f;
    public float wallRunSpeed = 8f;
    public float wallSlideAmount = 0.1f;

    // player ability toggles
    [Header("Player Abilties")]
    public bool canDoubleJump;
    public bool canTripleJump;
    public bool canWallJump;
    public bool canWallRun;
    public bool canWallSlide;

    // public state
    [Header("Player States")]
    public bool isJumping;
    public bool isDoubleJumping;
    public bool isTripleJumping;
    public bool isWallJumping;
    public bool isWallRunning;
    public bool isDucking;
    public bool isCreeping;
    
    // input flags
    private bool _startJump;
    private bool _releaseJump;

    private Vector2 _input;
    private Vector2 _moveDirection;
    private CharactorController2D _charactorController;
    private CapsuleCollider2D _capsuleCollider;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _originalColliderSize;
    private bool _ableToWallRun;

    private void Awake()
    {
        _charactorController = GetComponent<CharactorController2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _originalColliderSize = _capsuleCollider.size;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isWallJumping)
        {
            _moveDirection.x = _input.x * walkSpeed;

            // flipX 
            if (_moveDirection.x > 0f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (_moveDirection.x < 0f)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        if (_charactorController.below) // on the ground
        {
            _moveDirection.y = 0;
            isJumping = false;
            isDoubleJumping = false;   
            isTripleJumping = false;
            isWallJumping = false;
            isWallRunning = false;

            if(_startJump)
            {
                _startJump = false;
                _moveDirection.y = jumpSpeed;
                isJumping = true;
                _ableToWallRun = true;
                _charactorController.DisableGroundCheck(0.1f);
            }

            // 웅크리기, 기어가기 (duking, creeping)
            if (_input.y < 0f)
            {
                if (!isDucking && !isCreeping)
                {
                    isDucking = true;

                    _capsuleCollider.size = new Vector2(_capsuleCollider.size.x, _capsuleCollider.size.y / 2);
                    transform.position = new Vector2(transform.position.x, transform.position.y - (_originalColliderSize.y / 4));
                    _spriteRenderer.sprite = Resources.Load<Sprite>("directionSpriteUp_crouching");
                }
            }
            else
            {
                if (isCreeping || isDucking)
                {
                    RaycastHit2D hitCeiling = Physics2D.CapsuleCast(_capsuleCollider.bounds.center, transform.localScale,
                        CapsuleDirection2D.Vertical, 0f, Vector2.up, _originalColliderSize.y / 2, _charactorController.layerMask);

                    if (!hitCeiling.collider)
                    {
                        isDucking = false;
                        isCreeping = false;

                        _capsuleCollider.size = _originalColliderSize;
                        transform.position = new Vector2(transform.position.x, transform.position.y + (_originalColliderSize.y / 4));
                        _spriteRenderer.sprite = Resources.Load<Sprite>("directionSpriteUp");
                    }
                }
            }
            // creeping
            if (isDucking && _moveDirection.x != 0)
            {
                isCreeping = true;
            }
            else
            {
                isCreeping = false;
            }

        }
        else // 공중에
        {
            if(_releaseJump)
            {
                _releaseJump = false;

                if(_moveDirection.y > 0)
                {
                    _moveDirection.y *= 0.5f;
                }
            }
            
            // pressed jump buttonin air
            if (_startJump)
            {
                // tripple jumpping  // 트리플 점프를 먼저 체크해야 함
                if (canTripleJump && (!_charactorController.left && !_charactorController.right))
                {
                    if (isDoubleJumping && !isTripleJumping)
                    {
                        _moveDirection.y = doubleJumpSpeed;
                        isTripleJumping = true;
                    }
                }

                // double jumpping
                if (canDoubleJump && (!_charactorController.left && !_charactorController.right))
                {
                    if(!isDoubleJumping)
                    {
                        _moveDirection.y = doubleJumpSpeed;
                        isDoubleJumping = true;
                    }
                }

                // Wall jumpping
                if (canWallJump && (_charactorController.left || _charactorController.right))
                {
                    if (_charactorController.left && _moveDirection.x <= 0)
                    {
                        _moveDirection.x = xWallJumpSpeed;
                        _moveDirection.y = yWallJumpSpeed;
                        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    else if (_charactorController.right && _moveDirection.x >= 0)
                    {
                        _moveDirection.x = -xWallJumpSpeed;
                        _moveDirection.y = yWallJumpSpeed;
                        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                    isWallJumping = true;
                    StartCoroutine("WallJumpWaiter");
                }
                _startJump = false;
            }

            // wall running
            if (canWallRun && (_charactorController.left || _charactorController.right))
            {
                if(_input.y > 0f && _ableToWallRun)
                {
                    _moveDirection.y = wallRunSpeed;
                }
                // isWallRunning = true;
                StartCoroutine(WallRunWaiter());
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

        if (canWallSlide && (_charactorController.left || _charactorController.right))
        {
            if (_charactorController.hitWallThisFrame)
            {
                _moveDirection.y = 0f;
            }

            if(_moveDirection.y <= 0)
            {
                _moveDirection.y -= gravity * wallSlideAmount * Time.deltaTime;
            }
            else
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }
        }
        else
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }
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

    IEnumerator WallJumpWaiter()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(4f);
        isWallJumping = false;
    }

    IEnumerator WallRunWaiter()
    {
        isWallRunning = true;
        yield return new WaitForSeconds(0.5f);
        isWallRunning = false;
        if(!isWallRunning) _ableToWallRun = false;
    }
}
