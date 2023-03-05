using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeyContoller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float discountSpeed = 3f;
    [SerializeField] private float power = 20f;
    [SerializeField] private float gravityPower = 20f;

    Rigidbody2D rb;

    [SerializeField] private bool canJump = false;
    [SerializeField] private bool isJumping = false;

    public static bool isLeft = false;
    public static bool isRight = false;
    public static bool isUp = false;
    public static bool isDown = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = true;

        canJump = false;
        isJumping = false;
    }

    private void Update()
    {
        Movement();
        Gravity();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        else if(isJumping == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isUp) return;
            if(isRight) return;
            Jump();
            isRight = true;
            isDown = false;
            isLeft = false;
            isUp = false;
        }
        else if(isJumping == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(isLeft) return;
            if(isUp) return;
            Jump();
            isRight = false;
            isDown = false;
            isLeft = false;
            isUp = true;
        }
        else if(isJumping == true && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(isDown) return;
            if(isLeft) return;
            Jump();
            isRight = false;
            isDown = false;
            isLeft = true;
            isUp = false;
        }
        else if (isJumping == true && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(isRight) return;
            if(isDown) return;
            Jump();
            isRight = false;
            isDown = true;
            isLeft = false;
            isUp = false;
        }
    }

    private void Gravity()
    {
        if(isDown)
        {
            rb.AddRelativeForce(transform.up * -gravityPower);
        }
        
        if(isUp)
        {
            rb.AddRelativeForce(transform.up * gravityPower);
        }

        if(isLeft)
        {
            rb.AddRelativeForce(transform.right * -gravityPower);
        }

        if(isRight)
        {
            rb.AddRelativeForce(transform.right * gravityPower);
        }
    }

    private void Movement()
    {
        if(isRight)
        {
            if (isJumping == false)
            {
                rb.AddForce(transform.up * moveSpeed);
            }
            else if (isJumping == true)
            {
                rb.AddForce(transform.up * discountSpeed);
            }
        }

        if(isDown)
        {
            if (isJumping == false)
            {
                rb.AddForce(transform.right * moveSpeed);
            }
            else if (isJumping == true)
            {
                rb.AddForce(transform.right * discountSpeed);
            }
        }

        if(isUp)
        {
            if (isJumping == false)
            {
                rb.AddForce(transform.right * -moveSpeed);
            }
            else if (isJumping == true)
            {
                rb.AddForce(transform.right * -discountSpeed);
            }
        }

        if(isLeft)
        {
            if (isJumping == false)
            {
                rb.AddForce(transform.up * -moveSpeed);
            }
            else if (isJumping == true)
            {
                rb.AddForce(transform.up * -discountSpeed);
            }
        }
    }

    private void Jump()
    {
        if(canJump)
        {
            if(isLeft)
            {
                rb.AddForce(transform.right * power, ForceMode2D.Impulse);
                isJumping = true;
                canJump = false;
            }

            if(isRight)
            {
                rb.AddForce(transform.right * -power, ForceMode2D.Impulse);
                isJumping = true;
                canJump = false;
            }

            if(isUp)
            {
                rb.AddForce(transform.up * -power, ForceMode2D.Impulse);
                isJumping = true;
                canJump = false;
            }
             
            if(isDown)
            {
                rb.AddForce(transform.up * power, ForceMode2D.Impulse);
                isJumping = true;
                canJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "War")
        {
            canJump = true;
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Die();
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Left"))
        {
            GameManager.instance.CollisionToLeft = true;
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Left"))
        {
            GameManager.instance.CollisionToLeft = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Right"))
        {
            GameManager.instance.CollisionToRight = true;
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Right"))
        {
            GameManager.instance.CollisionToRight = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Down"))
        {
            GameManager.instance.CollisionToDown = true;
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Down"))
        {
            GameManager.instance.CollisionToDown = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Up"))
        {
            GameManager.instance.CollisionToUp = true;
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Up"))
        {
            GameManager.instance.CollisionToUp = false;
        }
    }

    

    public void Die()
    {
        GameManager.instance.GameOverEvent?.Invoke();
    }
}
