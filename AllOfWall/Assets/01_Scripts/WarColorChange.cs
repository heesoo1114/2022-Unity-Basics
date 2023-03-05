using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarColorChange : MonoBehaviour
{
    public GameObject LeftWar; 
    public GameObject RightWar; 
    public GameObject DownWar; 
    public GameObject UpWar; 

    private void Awake()
    {
        // spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ColorChange(GameObject war)
    {
        SpriteRenderer spriteRenderer = war.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }

    public void AllColorReset(GameObject war)
    {
        SpriteRenderer spriteRenderer = war.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }
}
