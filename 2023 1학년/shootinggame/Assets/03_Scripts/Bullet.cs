using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;



   

    
    void Update()
    {
        Vector3 dir = Vector3.up;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }
}
