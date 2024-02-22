using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class combatTransition : MonoBehaviour
{

    public string battleScene;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            print("Switching scene to " + battleScene);
            SceneManager.LoadScene(battleScene, LoadSceneMode.Single);
        }
    }
}
