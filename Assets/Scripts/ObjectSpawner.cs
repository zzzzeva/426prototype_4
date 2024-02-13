using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Assign your prefab in the inspector

    private GameObject currentObject = null; // Tracks the current spawned object
    private float minX, maxX, minY, maxY;

    void Start()
    {
        CalculateBounds();
        SpawnMovingObject(); // Initial spawn
    }

    void Update()
    {
        // If the current object is destroyed (null), spawn a new one
        if (currentObject == null)
        {
            SpawnMovingObject();
        }
    }

    void CalculateBounds()
    {
        // Find bounds by names or tags
        GameObject leftBound = GameObject.Find("LeftBound");
        GameObject rightBound = GameObject.Find("RightBound");
        GameObject topBound = GameObject.Find("TopBound");
        GameObject bottomBound = GameObject.Find("BottomBound");

        // Calculate the min and max X and Y from the bounds
        if (leftBound && rightBound && topBound && bottomBound)
        {
            BoxCollider2D leftCollider = leftBound.GetComponent<BoxCollider2D>();
            BoxCollider2D rightCollider = rightBound.GetComponent<BoxCollider2D>();
            BoxCollider2D topCollider = topBound.GetComponent<BoxCollider2D>();
            BoxCollider2D bottomCollider = bottomBound.GetComponent<BoxCollider2D>();

            minX = leftCollider.bounds.max.x;
            maxX = rightCollider.bounds.min.x;
            minY = bottomCollider.bounds.max.y;
            maxY = topCollider.bounds.min.y;
        }
    }

    void SpawnMovingObject()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        currentObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }
}
