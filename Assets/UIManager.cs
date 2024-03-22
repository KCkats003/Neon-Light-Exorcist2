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

    public GhostManager ghostManager;

    private void Start()
    {
        DataBaseUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            //Debug.Log("HIT");
            DataBaseUI.SetActive(true);

            ghostManager.ListItems();
        }
    }
    void OnTriggerExit(Collider other){
        DataBaseUI.SetActive(false);
    }
}
