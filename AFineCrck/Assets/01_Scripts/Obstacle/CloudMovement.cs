using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private Transform returnTr;

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
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

            if (transform.position.z < -850)
            {
                transform.position = returnTr.position;
            }

            yield return null;
        }
    }
}
