using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAppear : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI_Element;


    void Start()
    {
        UI_Element.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            UI_Element.SetActive(true);
        }


    }


    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            UI_Element.SetActive(false);
        }
       
    }

}
