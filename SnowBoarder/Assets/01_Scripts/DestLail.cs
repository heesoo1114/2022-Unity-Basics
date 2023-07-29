using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestLail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            dustEffect.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            dustEffect.Stop();
        }
    }
}
