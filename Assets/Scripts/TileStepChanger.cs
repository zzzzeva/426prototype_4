using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System;
using System.Linq;

public class TileStepChanger : MonoBehaviour
{
    public Tilemap tilemap;     // Assign in the inspector
    public int maxTileNumber;
    public int currentTile = 1;
    public Tile Tile_1;        // Assign the new tile to switch to
    private int tilesChangedCount1 = 0;  // Counter for the number of tiles changed
    public Tile Tile_2;
    private int tilesChangedCount2 = 0;
    public Tile Tile_3;
    private int tilesChangedCount3 = 0;
    public TextMeshProUGUI countDisplay;

    public ParticleSystem brushEffect_1;
    public ParticleSystem brushEffect_2;
    public ParticleSystem brushEffect_3;

    [SerializeField] int quota1;
    [SerializeField] int quota2;
    [SerializeField] int quota3;
    private AudioSource audioSource;
    [SerializeField] private AudioClip splat;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }
    }

    private void Update()
    {
        /*Vector3Int tilePosition = GetTilePositionUnderPlayer();
        ChangeTile(tilePosition);*/
        int ratio1 = (int)tilesChangedCount1 * 100 / maxTileNumber;
        int ratio2 = (int)tilesChangedCount2 * 100 / maxTileNumber;
        int ratio3 = (int)tilesChangedCount3 * 100 / maxTileNumber;

        countDisplay.text = "Green: " + ratio1 + "%/" + quota1 + "\n" + "Orange: " + ratio2 + "%/" + quota2 + "\n" + "Purple: " + ratio3 + "%/" + quota3+ "\n" +"SPACE to pick up Paint" + "\n" + "R to restart";


        //In the event the player wins. Should probably make it nicer.
        if (ratio1 >= quota1 && ratio2 >= quota2 && ratio3 >= quota3)
        {
            countDisplay.text = "You win!";

            Time.timeScale = 0.2f;

            audioSource.clip = splat;
            audioSource.Play();
            
            

            Queue<GameObject> queue = new Queue<GameObject>();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

            foreach (GameObject obstacle in obstacles) { queue.Enqueue(obstacle); }

            foreach (GameObject enemy in enemies) { queue.Enqueue(enemy); }
            foreach (GameObject pickup in pickups) { queue.Enqueue(pickup); }


            int safetyInt = 40;


            while (queue.Count > 0 && safetyInt > 0)
            {
                safetyInt--;
                GameObject obstacle = queue.Dequeue();
                Destroy(obstacle);
            }
            if (safetyInt == 0) { Debug.Log("Safety Int used"); }
        }
    }

    // Converts player's world position to tilemap position and returns it
    Vector3Int GetTilePositionUnderPlayer()
    {
        Vector2 playerPosition = transform.position;
        return tilemap.WorldToCell(playerPosition);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == tilemap.gameObject)
        {
            Bounds playerBounds = GetComponent<Collider2D>().bounds;
            Vector3Int minTile = tilemap.WorldToCell(playerBounds.min);
            Vector3Int maxTile = tilemap.WorldToCell(playerBounds.max);

            for (int x = minTile.x; x <= maxTile.x; x++)
            {
                for (int y = minTile.y; y <= maxTile.y; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    TileBase tile = tilemap.GetTile(tilePosition);
                    if (tile != null)
                    {
                        // Perform your operation here, e.g., change the tile
                        ChangeTile(tilePosition);
                    }
                }
            }
        }
    }

        // Changes the tile at the given position
        void ChangeTile(Vector3Int position)
    {
        if (tilemap.HasTile(position) && tilemap.GetTile(position) != Tile_1 && tilemap.GetTile(position) != Tile_2 && tilemap.GetTile(position) != Tile_3) // Original tile
        {

            if (currentTile == 1)
            {
                tilemap.SetTile(position, Tile_1);
                tilesChangedCount1++;  // Increment the counter
                PlayEffect(currentTile);
            }
            else if(currentTile == 2)
            {
                tilemap.SetTile(position, Tile_2);
                tilesChangedCount2++;
                PlayEffect(currentTile);
            }
            else
            {
                tilemap.SetTile(position, Tile_3);
                tilesChangedCount3++;
                PlayEffect(currentTile);
            }
        }
        else if(tilemap.HasTile(position) && tilemap.GetTile(position) != Tile_2 && tilemap.GetTile(position) != Tile_3)//is tile 1
        {
            if (currentTile == 3)
            {
                tilemap.SetTile(position, Tile_3);
                tilesChangedCount3++; 
                tilesChangedCount1--;
                PlayEffect(currentTile);
            }
            else if(currentTile == 2)
            {
                tilemap.SetTile(position, Tile_2);
                tilesChangedCount2++;
                tilesChangedCount1--;
                PlayEffect(currentTile);
            }
            
        }
        else if (tilemap.HasTile(position) && tilemap.GetTile(position) != Tile_1 && tilemap.GetTile(position) != Tile_3)//is tile 2
        {
            if (currentTile == 3)
            {
                tilemap.SetTile(position, Tile_3);
                tilesChangedCount3++;
                tilesChangedCount2--;
                PlayEffect(currentTile);
            }
            else if (currentTile == 1)
            {
                tilemap.SetTile(position, Tile_1);
                tilesChangedCount1++;
                tilesChangedCount2--;
                PlayEffect(currentTile);
            }
        }
        else if (tilemap.HasTile(position) && tilemap.GetTile(position) != Tile_1 && tilemap.GetTile(position) != Tile_2)//is tile 3
        {
            if (currentTile == 1)
            {
                tilemap.SetTile(position, Tile_1);
                tilesChangedCount1++;
                tilesChangedCount3--;
                PlayEffect(currentTile);
            }
            else if (currentTile == 2)
            {
                tilemap.SetTile(position, Tile_2);
                tilesChangedCount2++;
                tilesChangedCount3--;
                PlayEffect(currentTile);
            }
        }
        
    }

    private void PlayEffect(int currentTile)
    {
        if (currentTile == 1 && brushEffect_1 != null)
        {
            brushEffect_1.transform.position = transform.position;
            brushEffect_1.Play();
        }else if (currentTile == 2 && brushEffect_2 != null)
        {
            brushEffect_2.transform.position = transform.position;
            brushEffect_2.Play();
        }
        else if (currentTile == 3 && brushEffect_3 != null)
        {
            brushEffect_3.transform.position = transform.position;
            brushEffect_3.Play();
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}


