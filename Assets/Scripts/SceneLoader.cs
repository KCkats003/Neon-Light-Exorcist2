using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        foreach (string enemyName in GameManager.enemiesToDestroy)
        {
            GameObject enemyToDestroy = GameObject.Find(enemyName);
            if (enemyToDestroy != null)
            {
                Destroy(enemyToDestroy);
            }
        }

        // Now that all enemies are destroyed, update scene flags based on enemy count
        if (GameManager.enemiesToDestroy.Count == 0)
        {
            GameManager.actI = true;
        }
        else if (GameManager.enemiesToDestroy.Count == 2)
        {
            GameManager.actI = false;
            GameManager.actII = true;
        }
        else
        {
            GameManager.actII = false;
            GameManager.actIII = true;
        }

        Debug.Log(GameManager.enemiesToDestroy.Count);
        Debug.Log("SCENE 1 is: " + GameManager.actI);
        Debug.Log("SCENE 2 is: " + GameManager.actII);
        Debug.Log("SCENE 3 is: " + GameManager.actIII);
    }
}
