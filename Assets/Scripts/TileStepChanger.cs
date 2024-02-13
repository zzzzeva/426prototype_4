using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System;
using System.Linq;

public class TileStepChanger : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;     // Assign in the inspector
    public int currentTileMax = 3;
    public int tileAvailable = 3;
    private int TileCreated = 0;
    public int currentTile = 1;
    public Tile Tile1;
    public Tile Tile2;
    public float offset;

    public bool initiated = false;

    

    public ParticleSystem brushEffect;
    public ParticleSystem brushEffect_2;

    private AudioSource audioSource;
    [SerializeField] private AudioClip splat;
    private void Start()
    {
        audioSource= GetComponent<AudioSource>();
        tileAvailable = currentTileMax;
        initiated = true;
    }
    private void Update()
    {
        Vector3Int tilePosition = GetTilePositionUnderPlayer();
        ChangeTile(tilePosition);

        
    }

    // Converts player's world position to tilemap position and returns it
    Vector3Int GetTilePositionUnderPlayer()
    {
        Vector2 playerPosition = transform.position - new Vector3(0f, offset, 0f);
        return tilemap.WorldToCell(playerPosition);
    }


        // Changes the tile at the given position
        private void ChangeTile(Vector3Int position){
        if (!tilemap.HasTile(position) && tileAvailable>0) // Original tile
        {
            if (tileAvailable==1) //rewarding sound effects
            {
                //Debug.Log("tileAvailable is "+ tileAvailable);
                tilemap.SetTile(position, Tile2);
                PlayEffect(2);
                PlayEffect(2);
                audioSource.clip = splat; 
                audioSource.Play();
            }
            else
            {
                //Debug.Log("tileAvailable is " + tileAvailable);
                tilemap.SetTile(position, Tile1);
                PlayEffect(1);
                audioSource.clip = splat;
                audioSource.Play();
            }
                tileAvailable--;  // Increment the counter
                TileCreated++;
                
        }
    }

    private void PlayEffect(int currentTile)
    {
        if (currentTile == 1 && brushEffect != null)
        {
            brushEffect.transform.position = transform.position;
            brushEffect.Play();
        }else if(currentTile == 2 && brushEffect_2 != null)
        {
            brushEffect_2.transform.position = transform.position;
            brushEffect_2.Play();
        }

        /*if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }*/
    }
}


