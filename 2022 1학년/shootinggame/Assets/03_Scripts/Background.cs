using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    //public Material bgmat;
    public MeshRenderer bgMeshRenderer;
    public float scrollSpeed = 0.2f;

    
    // Start is called before the first frame update
    void Start()
    {
        //bgmat = GetComponent<Material>();
        bgMeshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 dir = Vector2.up;


        // bgmat.mainTextureOffset += dir * scrollSpeed * Time.deltaTime;

        bgMeshRenderer.material.mainTextureOffset
        += dir * scrollSpeed * Time.deltaTime;





    }
}
