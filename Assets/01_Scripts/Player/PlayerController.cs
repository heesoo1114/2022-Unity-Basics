using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigid;


    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;
    [SerializeField] private float sideSpeed;

    [SerializeField] private float acceleration;   // 가속도
    [SerializeField] private float deAcceleration; // 감속도

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveSpeed = AccelSpeed();
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * sideSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * sideSpeed * Time.deltaTime;
        }
    }

    private float AccelSpeed()
    {
        moveSpeed += acceleration * Time.deltaTime;

        return Mathf.Clamp(moveSpeed, 0, maxSpeed);
    }

    private float DecelSpeed()
    {
        moveSpeed -= deAcceleration * Time.deltaTime;

        return Mathf.Clamp(moveSpeed, 0, maxSpeed);
    }

    private void StopImmediatelly()
    {
        moveSpeed = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        moveSpeed = DecelSpeed();
    }
}
