using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigid;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

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

        RoatateAircraft(z);
        MoveAircraft(z);
    }

    private void MoveAircraft(float x)
    {
        Vector3 movement = new Vector3(x, 0f, 0f);

        _rigid.velocity = movement * moveSpeed;
    }

    private void RoatateAircraft(float z)
    {
        Vector3 lerpVector = new Vector3(0, 0, -z * rollAmount);
        rotateValue = Vector3.Lerp(rotateValue, lerpVector, lerpAmount * Time.fixedDeltaTime);

        _rigid.MoveRotation(_rigid.rotation * Quaternion.Euler(rotateValue * Time.fixedDeltaTime));
    }

    private void StopImmediatelly()
    {
        moveSpeed = 0;
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
