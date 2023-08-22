using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;

   
    void Update()
    {
        // -1 ~ 1
        float h = Input.GetAxis("Horizontal");
       
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);
        transform.Translate(dir.normalized * speed * Time.deltaTime);

    }
}
