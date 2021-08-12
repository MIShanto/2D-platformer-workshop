using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject enemyDestroyParticlesEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {

            Destroy(collision.gameObject);

            GameObject particle =  Instantiate(enemyDestroyParticlesEffect, transform.position, Quaternion.identity);

            Destroy(particle, 2f);

            Destroy(gameObject);
        }

        if (collision.tag == "Destructor")
        {
            
            Destroy(gameObject);
        }
    }
}