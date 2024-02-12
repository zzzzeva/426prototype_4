using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthMover : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float movementDistance = 3f; // Maximum distance to move from the start position

    private Vector2 startPosition;
    private bool movingRight = true; // Direction of movement

    void Start()
    {
        // Record the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the current movement direction
        Vector2 moveDirection = movingRight ? Vector2.right : Vector2.left;

        // Move the object
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Check if the object has reached the boundary of its movement range
        if (movingRight && transform.position.x >= startPosition.x + movementDistance)
        {
            movingRight = false; // Change direction
        }
        else if (!movingRight && transform.position.x <= startPosition.x - movementDistance)
        {
            movingRight = true; // Change direction
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//sfx needed
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Switch>().ScoreUp();
            Destroy(gameObject);
        } else
        {
            if (movingRight)
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;
            }
        }
        
    }
}

