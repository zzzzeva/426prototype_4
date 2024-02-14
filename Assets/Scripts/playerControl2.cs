using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float projectileSpeed;
    private double nextFireTime;
    public static float fireCooldown = 2;

    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject innerCircle;

    private AudioSource playerAudio;
    public AudioClip pop;
    public ParticleSystem bubbleEffect;
    public AudioSource popSound;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAudio.clip = pop;
        nextFireTime = Time.time; // Initialize next fire time
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate movement based on numpad keys
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.Keypad8)|| Input.GetKey(KeyCode.UpArrow))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.Keypad6) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;

        // Move the GameObject
        transform.Translate(movement);

        // Rotate the pointer around the circle sprite
        if (Input.GetKey(KeyCode.Keypad9) || Input.GetKey(KeyCode.RightShift))
        {
            pointer.transform.RotateAround(transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad7) || Input.GetKey(KeyCode.Slash))
        {
            pointer.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // Fire projectile if cooldown has passed
        if (Input.GetKeyDown(KeyCode.Keypad0) && Time.time >= nextFireTime || Input.GetKey(KeyCode.Return) && Time.time >= nextFireTime)
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

        projectile.GetComponent<bubbleControl2>().bubbleEffect = bubbleEffect;
        projectile.GetComponent<bubbleControl2>().bubbleAudio = popSound;
        // Calculate direction towards pointer
        Vector3 direction = (pointer.transform.position - transform.position).normalized;

        // Apply force to the projectile in the direction of the pointer
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        playerAudio.Play();
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
