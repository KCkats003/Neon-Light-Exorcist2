using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {


        Debug.Log("SCENE 2 is: " + GameManager.actII);

        foreach (string enemyName in GameManager.enemiesToDestroy)
        {
            GameObject enemyToDestroy = GameObject.Find(enemyName);
            if (enemyToDestroy != null)
            {
                Destroy(enemyToDestroy);

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
            }
        }
    }
}
