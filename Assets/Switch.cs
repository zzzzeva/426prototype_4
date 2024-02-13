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

    private TilemapCollider2D tilemapCollider;
    public TextMeshProUGUI countDisplay;
    public TextMeshProUGUI scoreDisplay;
    private int score = 0;

    // Update is called once per frame
    private void Start()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        wallControl = gameObject.GetComponent<PlayerMovement>();
        groundControl = gameObject.GetComponent<PlayerController>();
        tileChange = gameObject.GetComponent<TileStepChanger>();
    }
    void Update()
    {
        // Check if the shift key was pressed this frame
        if (Input.GetKey(KeyCode.LeftShift)|| Input.GetMouseButton(0))
        {
            Debug.Log("switching");
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

        if (tileChange.tileAvailable == 0)
        {
            StartCoroutine(SwitchToGround());
            
        }
    }
    IEnumerator SwitchToGround()
    {
        
        wallControl.enabled = false;
        tileChange.enabled = false;
        tilemapCollider.enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        groundControl.enabled = true;
        yield return new WaitForSeconds(1.5f);
        tileChange.tileAvailable = tileChange.currentTileMax;
    }

    public void ScoreUp()
    {
        score++;
    }

}


