using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempGhostMove : MonoBehaviour
{
    public bool movement;
    public float movementSpeed;
    public float movementAmount; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //og up 2.7
        //og down 1.5
        //movementAmount
        if (transform.position.y > 2.7)
        {
            movement = false;
        }
        else if (transform.position.y < 1.5) {
            movement = true;
        }
       

        if(movement)
        {
            transform.position = transform.position + new Vector3(0, movementSpeed * Time.deltaTime, 0);
        }
        else {
            transform.position = transform.position + new Vector3(0, -movementSpeed * Time.deltaTime, 0);
        }
    }
}
