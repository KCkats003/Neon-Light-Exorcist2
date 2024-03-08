using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject hitbox;
    private BoxCollider boxCol;
    [SerializeField]
    private GameObject DataBaseUI;
    
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Debug.Log("HIT");
            DataBaseUI.SetActive(true);
        }
        else{
            
        }
    }
    void OnTriggerExit(Collider other){
        DataBaseUI.SetActive(false);
    }
    
}
