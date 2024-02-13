using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.UI.Image;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] private float screenShakeIntensity = 0.2f;
    [SerializeField] private float screenShakeMinimumTime = 0.5f;

    private ParticleSystem playerParticles;
    public GameObject CameraGet;

    /*AudioSource playerAudio;
    public AudioClip splat;*/

    private void Awake()
    {
        //playerAudio = GetComponent<AudioSource>();
        //CameraGet = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        Move();
    }

    /* Method name: Move
     * Description: for regular movements*/
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal_p1");
        float vertical = Input.GetAxis("Vertical_p1");

        Vector2 movement = new Vector2(horizontal, vertical);
        movement.Normalize();

        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }
    
}
