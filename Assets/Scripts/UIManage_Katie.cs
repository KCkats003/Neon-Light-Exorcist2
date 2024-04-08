using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_Katie : MonoBehaviour
{
    [SerializeField]
    public GameObject hitbox;
    private BoxCollider boxCol;
    [SerializeField]
    private GameObject DataBaseUI;

    public GhostManager_Katie ghostManager;

    private void Start()
    {
        DataBaseUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            DataBaseUI.SetActive(true);
            ghostManager.ListItems();
        }
    }
    void OnTriggerExit(Collider other){
        DataBaseUI.SetActive(false);
    }
}
