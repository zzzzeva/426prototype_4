using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalControl2 : MonoBehaviour
{
    public float playerHealth;
    public GameObject healthBar;

    private float maxHealth;
    private float indicator;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = playerHealth;
        healthBar.transform.localScale = new Vector3(0.8f, 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged "Player 1"
        if (collision.gameObject.CompareTag("Player 1"))
        {
            // Decrease player's health upon collision with the projectile
            playerHealth -= 1;
            indicator = (maxHealth - playerHealth) / 10;
            healthBar.transform.localScale = new Vector3(0.8f, indicator, 1f);

            if (playerHealth == 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
