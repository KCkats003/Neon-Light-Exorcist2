using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float groundDist;
    private bool canMove = true;  // New variable to control movement

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos; 
            }
        }

        if (canMove)
        {
            // Get input from player
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            // Move the player
            Vector3 movement = new Vector3(x, 0, y);
            rb.velocity = movement * moveSpeed;

            if (x != 0 && x < 0)
            {
                sr.flipX = false;
            } 
            else if (x != 0 && x > 0)
            {
                sr.flipX = true;
            }
        }
        else
        {
            // Freeze X-axis movement
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
}

    // Method to set the player's ability to move
    public void SetCanMove(bool move)
    {
        canMove = move;
    }

    // Triggered when the player collides with another collider
}
