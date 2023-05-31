using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    private void Update()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
