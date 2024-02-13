using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    public int playerIndex;
    public float jumpForce = 700f; // Adjust this value to change the jump height
    //private bool isGrounded = false;
    private ParticleSystem playerParticles;
    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private AudioSource audioSource;
    public AudioClip jump;

    //public int bubbleTotal = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal_p1");

        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.clip = jump;
            audioSource.Play();
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
            //vertical = Input.GetAxis("Vertical_p1");
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal_p2");
            //vertical = Input.GetAxis("Vertical_p2");
        }
        

        //Vector2 movement = new Vector2(horizontal, vertical);
        //movement.Normalize();

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal, 0f) * speed;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


}