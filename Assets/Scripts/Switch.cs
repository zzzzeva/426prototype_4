using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Switch : MonoBehaviour
{
    public Tilemap tilemap;
    private PlayerMovement wallControl;
    private PlayerController groundControl;
    private TileStepChanger tileChange;

    private AudioSource audioSource;
    public AudioClip positiveFeedback;

    private TilemapCollider2D tilemapCollider;
    public TextMeshProUGUI countDisplay;
    public TextMeshProUGUI scoreDisplay;
    private int score = 0;

    public TimerManager timer;

    public ParticleSystem deathEffect;

    private bool isCoroutineRunning;

    // Update is called once per frame
    private void Start()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        wallControl = gameObject.GetComponent<PlayerMovement>();
        groundControl = gameObject.GetComponent<PlayerController>();
        tileChange = gameObject.GetComponent<TileStepChanger>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Check if the shift key was pressed this frame
        if (Input.GetMouseButton(0) && !isCoroutineRunning)
        {
            
            // Toggle the enabled state of the scripts
            wallControl.enabled = true;
            tileChange.enabled = true;
            tilemapCollider.enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            groundControl.enabled = false;
        }
        else if(!groundControl.enabled)
        {
            StartCoroutine(SwitchToGround());
        }

        countDisplay.text = "Current tile remained: " + tileChange.tileAvailable + "\n" + "Max number of tile at one time: " + tileChange.currentTileMax + "\n" + "Hold LeftMouse to build" + "\n" + "R to restart";
        scoreDisplay.text = "Score: " + score;

        if (tileChange.tileAvailable == 0 && tileChange.initiated && !isCoroutineRunning)
        {
            Debug.Log("switch to ground" + tileChange.tileAvailable);
            StartCoroutine(SwitchToGround());
        }
    }
    IEnumerator SwitchToGround()
    {
        isCoroutineRunning = true;
        wallControl.enabled = false;
        tileChange.enabled = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.02f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        tilemapCollider.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        groundControl.enabled = true;
        yield return new WaitForSeconds(1.5f);
        isCoroutineRunning = false;
        tileChange.tileAvailable = tileChange.currentTileMax;
    }

    public void ScoreUp()
    {
        score++;
        tileChange.currentTileMax++;//positive feedback
        timer.StartOrResetTimer();
        audioSource.clip = positiveFeedback;
        audioSource.Play();
    }

    public void EndGame()
    {
        wallControl.enabled = false;
        tileChange.enabled = false;
        tilemapCollider.enabled = false;
        groundControl.enabled = false;
        deathEffect.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void WinGame()
    {
        wallControl.enabled = false;
        tileChange.enabled = false;
        groundControl.enabled = false;
        deathEffect.Play();
        audioSource.clip = positiveFeedback;
        audioSource.Play();
    }
}


