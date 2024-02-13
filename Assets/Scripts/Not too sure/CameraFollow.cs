using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset from the player's position
    public float smoothSpeed = 0.125f; // How smoothly the camera catches up to its target movement
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
        //transform.LookAt(player);
    }
}

