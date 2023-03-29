using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float moveSpeed = 0f;
    private Vector3 dir = Vector3.right;

    private void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        if (transform.position.x >= 5)
        {
            dir = Vector3.left;
        }
        else if (transform.position.x <= -5)
        {
            dir = Vector3.right;
        }

        transform.Translate(moveSpeed * dir * Time.deltaTime);
    }
}
