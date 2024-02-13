using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float projectileSpeed;
    private double nextFireTime;
    public static double fireCooldown = 2;
    

    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        nextFireTime = Time.time; // Initialize next fire time
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate movement based on WASD keys
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

        // Move the GameObject
        transform.Translate(movement);

        // Rotate the pointer around the circle sprite
        if (Input.GetKey(KeyCode.E))
        {
            pointer.transform.RotateAround(transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            pointer.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // Fire projectile if cooldown has passed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireCooldown; // Set next fire time
        }
    }

    void FireProjectile()
    {
        // Instantiate projectile at the position of the pointer
        GameObject projectile = Instantiate(projectilePrefab, pointer.transform.position, Quaternion.identity);

        // Calculate direction towards pointer
        Vector3 direction = (pointer.transform.position - transform.position).normalized;

        // Apply force to the projectile in the direction of the pointer
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}
