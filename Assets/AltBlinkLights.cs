using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltBlinkLights : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
        if (isFlickering == false)
        {
            StartCoroutine(delay());
            //StartCoroutine(BlinkLight());
        }
        IEnumerator BlinkLight()
        {
            isFlickering = true;
            yield return new WaitForSeconds(timeDelay);
            if (this.gameObject.GetComponent<Light>().enabled == false)
            {
                this.gameObject.GetComponent<Light>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<Light>().enabled = false;
            }
            isFlickering = false;
        }

        IEnumerator delay()
        {
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(BlinkLight());
        }
    }
}

//this.gameObject.GetComponent<Light>().enabled = false;
//yield return new WaitForSeconds(timeDelay);
//this.gameObject.GetComponent<Light>().enabled = true;
//yield return new WaitForSeconds(timeDelay);