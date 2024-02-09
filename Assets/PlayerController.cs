using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    public int playerIndex;

    private ParticleSystem playerParticles;
    //public GameObject CameraGet;

    void Update()
    {

        Move();

    }

    /* Method name: Move
     * Description: for regular movements*/
    private void Move()
    {
        if(playerIndex !=1 && playerIndex !=2)
        {
            Debug.LogError("Invalid player number");
            return;
        }

        float horizontal, vertical;
        if (playerIndex == 1)
        {
            horizontal = Input.GetAxis("Horizontal_p1");
            vertical = Input.GetAxis("Vertical_p1");
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal_p2");
            vertical = Input.GetAxis("Vertical_p2");
        }
        

        Vector2 movement = new Vector2(horizontal, vertical);
        movement.Normalize();

        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }

}