using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed 
    { 
        get => moveSpeed; 
        set => moveSpeed = value; 
    }

    private void Start()
    {
        StartCoroutine(DownMove());
    }

    IEnumerator DownMove()
    {
        while (true)
        {
            transform.Translate(Vector3.back *  moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}
