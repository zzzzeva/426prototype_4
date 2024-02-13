using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class CircleGrow: MonoBehaviour
{
    public GameObject circlePrefab;
    public PlayerController playerController;
    public float growRate = 0.5f; 
    private bool isPressed = false;
    private bool canGrow = true;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // check if clicked on circle
            Debug.Log("clicked");
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider == null)
            {
                GameObject temp = Instantiate(circlePrefab, pos, Quaternion.identity);
                playerController.bubbleTotal++;
                temp.GetComponent<CircleGrow>().playerController = playerController;
                isPressed = true;
            }
            else if (hit.collider.gameObject == gameObject)
            {
                isPressed = true;
            }
        }

        // 检查鼠标左键是否释放
        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }

        // 如果正在被按下，增加圆形的大小
        if (isPressed && canGrow)
        {
            transform.localScale += Vector3.one * growRate * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure your player GameObject has the tag "Player"
        {
            // Disable gravity for the player or notify the player's script to change its gravity handling
            //playerController.InsideBubble(true);
            canGrow = false;
        }
        else
        {
            Debug.Log("delete-ing");
            transform.localScale += Vector3.one * growRate;
            //if (!canGrow)//if collide with other gameobject
            //{
            //    playerController.InsideBubble(false);
            //}
            playerController.bubbleTotal--;
            Destroy(gameObject);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Re-enable gravity or notify the player's script to revert its gravity handling
            //playerController.InsideBubble(false);
            canGrow = true;
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //    {
    //        canGrow = false; // 停止增长
    //        Destroy(gameObject);
    //    }

        //void OnCollisionExit2D(Collision2D collision)
        //{
        //    canGrow = true; 
        //}
}
