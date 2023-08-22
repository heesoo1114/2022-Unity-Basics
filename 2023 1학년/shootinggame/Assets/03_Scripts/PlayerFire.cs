using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public GameObject bulletFactory;
    public GameObject firePosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼쪽 클릭하면 총알 발사
        if(Input.GetButtonDown("Fire1"))
        {
           GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position; 
        }
    }
}
