using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;
    public float torqueAmount = 1f;

    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;

    SurfaceEffector2D _effector;
    public bool isDead = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _effector = FindObjectOfType<SurfaceEffector2D>();
    }

    private void Update()
    {
        if (isDead == false)
        {
            Rotateplayer();
            RespondToBoost();
        }
        CantControll();
    }

    private void CantControll()
    {
        if(isDead == true)
        {
            _effector.enabled = false;
        }
    }

    private void RespondToBoost()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            _effector.speed = boostSpeed;
        }
        else
        {
            _effector.speed = baseSpeed;
        }
    }

    private void Rotateplayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigid.AddTorque(torqueAmount);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigid.AddTorque(-torqueAmount);
        }
    }
}
