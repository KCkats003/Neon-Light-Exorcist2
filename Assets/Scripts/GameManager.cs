using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Vector3 playerStartPosition;
    public static int playerHealth = 50;
    public static string objectNameToDestroy;
    public static List<string> enemiesToDestroy = new List<string>();
    public static bool nextAct;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
      //Debug.Log("Player Health: " + playerHealth);
    }

    public static void DestroyObjectInSampleScene()
    {
        // Find and destroy the object in the sample scene by its name
        GameObject objectToDestroy = GameObject.Find(objectNameToDestroy);
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }

    public static void AddEnemyToDestroy(string enemyName)
    {
        enemiesToDestroy.Add(enemyName);
    }
}
