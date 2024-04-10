using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class PlayerController_Katie : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float groundDist;
    private bool canMove = true;  // New variable to control movement

     // New variable to determine if player position should be set
    private List<string> combatSceneNames = new List<string>();

    //public GameObject animation_1;
    public GameObject Static;

    public LayerMask terrainLayer;
    public Rigidbody rb;

    public GameObject player; 
        
    private SpriteRenderer ExploreSpriteRenderer;

    private Animator ExploreAnimator;


    //private Vector3 initialSpawnPosition;

    private void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        //BGMusic
        FindObjectOfType<AudioManager>().PlaySound("BGMusic");

        ExploreAnimator = player.GetComponent<Animator>();

        // SpriteRenderer anim_sr = animation_1.GetComponent<SpriteRenderer>();
        ExploreSpriteRenderer = player.GetComponent<SpriteRenderer>();


        if (GameManager.shouldSetPosition == true)
        {
            // Set the player's position to the last saved position
            transform.position = GameManager.playerStartPosition;
            GameManager.shouldSetPosition = false; // Reset the flag
        }

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


            if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)))
            {
                //print("left arrow key is held down");
                ExploreAnimator.SetTrigger("WalkSide");
                // sr.enabled = false;
                //animation_1.SetActive(true);
            }

            else if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D)))
            {
                //print("right arrow key is held down");
                ExploreAnimator.SetTrigger("WalkSide");
                // sr.enabled = false;
                // animation_1.SetActive(true);
            }
            else if ((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W)) )
            {
                //print("up or down key is held down");
                ExploreAnimator.SetTrigger("WalkBack");
            }

            else if ((Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.S)))
                {
                ExploreAnimator.SetTrigger("WalkFront");
            }
            else
            {
                ExploreAnimator.SetTrigger("Idle");
                // sr.enabled = true;
                // animation_1.SetActive(false);
            }
           
         //   

            if (x != 0 && x < 0)
            {
                // sr.flipX = false;

                ExploreSpriteRenderer.flipX = true;

              //  sr.enabled = false;
            } 
            else if (x != 0 && x > 0)
            {
                //  sr.flipX = true;

                ExploreSpriteRenderer.flipX = false;
              //  sr.enabled = false;
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
}
