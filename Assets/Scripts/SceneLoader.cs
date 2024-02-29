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

            Debug.Log(enemyName);

            GameObject enemyToDestroy = GameObject.Find(enemyName);
            if (enemyToDestroy != null)
            {
                Destroy(enemyToDestroy);
            }
        }
    }
}
