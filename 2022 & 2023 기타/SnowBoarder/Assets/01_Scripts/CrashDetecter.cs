using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetecter : MonoBehaviour
{
    [SerializeField] private float delayTime = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" && playerController.isDead == false)
        {
            crashEffect.Play();
            Invoke("RoadScene", delayTime);
            playerController.isDead = true;
        }
    }

    private void RoadScene()
    {
        SceneManager.LoadScene("Level1");
        playerController.isDead = false;
    }
}
