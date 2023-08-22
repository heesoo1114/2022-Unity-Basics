using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public GameObject explosionFactory;
    public float speed = 5;
    Vector3 dir;

    

    private void Start()
    {
        int randValue = Random.Range(0,10);  // 0~9까지 나옴

        if(randValue < 3)   // 30프로 확률 플레이어 방향으로 가시오
        {
            GameObject target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        else   // 아래로 가시오
        {
            dir = Vector3.down;
        }
    }


    void Update()
    {

        transform.Translate(dir * speed * Time.deltaTime);
        

        
    }

    private void OnCollisionEnter(Collision collision)
    {

        GameObject smObject = GameObject.Find("ScoreManager");
        ScoreManager sm = smObject.GetComponent<ScoreManager>();
        sm.SetScore(sm.GetScore() + 1);

        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        Destroy(gameObject);
        Destroy(collision.gameObject);
       
        
         
            if(collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("GameOver");
            }
        
    }

    
}
