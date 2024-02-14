using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float projectileSpeed;

    private double nextFireTime;
    public static float fireCooldown = 2;
    

    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject innerCircle;
    

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
            StartCoroutine(ScaleOverTime(innerCircle, fireCooldown));
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

    IEnumerator ScaleOverTime(GameObject innerCircle, float duration)
    {
        Vector3 originalScale = innerCircle.transform.localScale; // Assuming the original scale might not be Vector3.zero
        Vector3 zeroScale = Vector3.zero;

        float time = 0;
        while (time < duration)
        {
            innerCircle.transform.localScale = Vector3.Lerp(zeroScale, originalScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is set to the target scale when done
        innerCircle.transform.localScale = originalScale;
    }
}
