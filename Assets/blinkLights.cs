using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkLights : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(BlinkLight());
        }
        IEnumerator BlinkLight()
        {
            isFlickering = true;

            yield return new WaitForSeconds(timeDelay);
            if (this.gameObject.GetComponent<Light>().enabled == false)
            {
                this.gameObject.GetComponent<Light>().enabled = true;
            }
            else {
                this.gameObject.GetComponent<Light>().enabled = false;
            }

            isFlickering = false;
        }
    }
}

/**
 *            this.gameObject.GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(timeDelay);
            this.gameObject.GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(timeDelay); **/