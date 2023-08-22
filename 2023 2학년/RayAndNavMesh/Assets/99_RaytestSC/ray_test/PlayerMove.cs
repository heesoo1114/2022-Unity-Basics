using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(h, 0, y);
        _rigid.velocity = velocity * speed;

        transform.LookAt(transform.position + velocity);
    }
}
