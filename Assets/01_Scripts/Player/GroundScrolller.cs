using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScrolller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        StartCoroutine(GroundMove());
    }

    IEnumerator GroundMove()
    {
        while (true)
        {
            transform.Translate(Vector3.back *  moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
}
