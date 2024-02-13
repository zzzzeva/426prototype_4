using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        // Find all GameObjects with the tag "Player 1"
        GameObject[] player1Objects = GameObject.FindGameObjectsWithTag("Player 1");

        // Calculate fireCooldown for Player 1 based on the number of Player 1 objects
        float fireCooldown1 = Mathf.Max(0, 2.0f - (player1Objects.Length * 0.25f)); // Minimum cooldown is 0

        // Set the fireCooldown in playerControl1 script
        playerControl1.fireCooldown = fireCooldown1;

        // Find all GameObjects with the tag "Player 2"
        GameObject[] player2Objects = GameObject.FindGameObjectsWithTag("Player 2");

        // Calculate fireCooldown for Player 2 based on the number of Player 2 objects
        float fireCooldown2 = Mathf.Max(0, 2.0f - (player2Objects.Length * 0.25f)); // Minimum cooldown is 0

        // Set the fireCooldown in playerControl2 script
        playerControl2.fireCooldown = fireCooldown2;
    }
}
