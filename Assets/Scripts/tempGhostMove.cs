using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempGhostMove : MonoBehaviour
{
    public bool movement;
    public float movementSpeed;
    public float movementAmount;

   private  Vector3 StartPos; 

   // private float startPos; 
    // Start is called before the first frame update
    void Start()
    {
        // startPos = 2.7f;

        StartPos = transform.position;
        if (movementAmount == 0.0f)
        {
            movementAmount = 1.2f;
        }

        if (movementSpeed == 0.0f)
        {
            movementSpeed = 0.15f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //og up 2.7
        //og down 1.5
        //movementAmount1.2
        /*
                if (movementAmount == 0.0f) {
                    movementAmount = 1.2f;
                }
        */

        /*
        if (transform.position.y > (startPos - movementAmount))
        {
            movement = false;
        }
        else if (transform.position.y < startPos) {
            movement = true;
        }*/
        //t.localPosition

        /*
        Transform t = transform;
        if (t.localposition.y > 2.7)
        {
            movement = false;
        }
        else if (t.localposition.y < 1.5)
        {
            movement = true;
        }
        //1.5

        */


        /*

        Debug.Log(transform.localPosition.y);
        if (transform.localPosition.y > 1.5)
        {
            movement = false;
        }
        else if (transform.localPosition.y < (1.5 - movementAmount))
        {
            movement = true;
        }

        if (movement)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, movementSpeed * Time.deltaTime, 0);
        }
        else {
            transform.localPosition = transform.localPosition + new Vector3(0, -movementSpeed * Time.deltaTime, 0);
        }




        */





        if (transform.position.y > (StartPos.y + movementAmount))
        {
            movement = false;
        }
        else if (transform.position.y < (StartPos.y - movementAmount))
        {
            movement = true;
        }


        if (movement)
        {
            transform.position = transform.position + new Vector3(0, movementSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = transform.position + new Vector3(0, -movementSpeed * Time.deltaTime, 0);
        }

    }
}
