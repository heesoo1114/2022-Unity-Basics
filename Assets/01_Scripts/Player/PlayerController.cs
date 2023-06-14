using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigid;

    [Header("Movement")]
    [SerializeField] private float frontSpeed;
    public float FrontSpeed => frontSpeed;
    public float sideSpeed;


    [Header("Dash")]
    public float dashSpeed;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    [Header("Roatation")]
    public float rollAmount;
    Vector3 rotateValue;
    public float lerpAmount;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashMovement(z);
        }

        transform.Translate(Vector3.forward * frontSpeed * Time.deltaTime, Space.World);

        RotationPlane(z);
        HorizontalMovement(z);
    }

    private void HorizontalMovement(float x)
    {
        Vector3 movement = new Vector3(x, 0f, 0f);
        _rigid.velocity = movement * sideSpeed;
    }

    private void RotationPlane(float z)
    {
        Vector3 lerpVector = new Vector3(0, 0, -z * rollAmount);
        rotateValue = Vector3.Lerp(rotateValue, lerpVector, lerpAmount * Time.deltaTime);
        _rigid.MoveRotation(_rigid.rotation * Quaternion.Euler(rotateValue * Time.fixedDeltaTime));
    }

    private void DashMovement(float z)
    {
        Vector3 dashDir = (z == 1) ? Vector3.right : Vector3.left;

        // _rigid.AddForce(dashDir * dashSpeed, ForceMode.Impulse); 
        // _rigid.velocity = dashDir * dashSpeed;

        StartCoroutine(Dash(dashDir));
    }

    private IEnumerator Dash(Vector3 dashDir)
    {
        isDashing = true;
        Vector3 dashDirection = dashDir; 
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float dashSpeedCurrent = Mathf.Lerp(dashSpeed, 0f, elapsedTime / dashDuration);

            _rigid.velocity = dashDirection * dashSpeedCurrent;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rigid.velocity = Vector3.zero;
        isDashing = false;
    }

    private void StopImmediatelly()
    {
        frontSpeed = 0;
        sideSpeed = 0;
        dashSpeed = 0;
    }

    #region Accel,Decel
    // private float AccelSpeed()
    // {
    //     moveSpeed += acceleration * Time.deltaTime;
    // 
    //     return Mathf.Clamp(moveSpeed, 0, maxSpeed);
    // }
    // 
    // private float DecelSpeed()
    // {
    //     moveSpeed -= deAcceleration * Time.deltaTime;
    // 
    //     return Mathf.Clamp(moveSpeed, 0, maxSpeed);
    // }
    #endregion
}
