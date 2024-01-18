using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class combatTransition : MonoBehaviour
{
    public string battleScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Switching scene to " + battleScene);
            SceneManager.LoadScene(battleScene, LoadSceneMode.Single);
        }
    }
}
