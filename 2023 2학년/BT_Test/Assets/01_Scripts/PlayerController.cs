using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private ParticleSystem effect;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 targetPos = new Vector3(h, 0, v);
        transform.Translate(targetPos * moveSpeed * Time.deltaTime); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        effect.Play();
    }
}
