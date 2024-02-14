using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleControl1 : MonoBehaviour
{
    public AudioSource bubbleAudio;
    public ParticleSystem bubbleEffect;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged "Player 2"
        if (collision.gameObject.CompareTag("Player 2"))
        {
            // Destroy the projectile
            bubbleAudio.Play();
            if (bubbleEffect != null)
            {
                bubbleEffect.transform.position = transform.position;
                bubbleEffect.Play();

            }
            Destroy(gameObject);
        }
    }
}
