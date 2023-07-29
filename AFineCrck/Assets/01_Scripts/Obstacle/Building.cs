using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Building : PoolAbleMono
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed 
    { 
        get => moveSpeed; 
        set => moveSpeed = value; 
    }

    private void OnEnable()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Garbage"))
        {
            PoolManager.Instance.Push(this);
        }
    }

    public override void Init()
    {
        // do nothing
    }
}
