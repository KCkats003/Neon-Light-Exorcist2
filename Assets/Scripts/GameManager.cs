using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Vector3 playerStartPosition;
    public static int playerHealth = 50;
    public static string objectNameToDestroy;
    public static bool ghostDefeated = false;
    public static List<string> enemiesToDestroy = new List<string>();
    public static bool nextAct;

    public static bool actI;
    public static bool actII;
    public static bool actIII;


    //The gameobject will store the ghosts going into combat, while Ghost will store info for the UI
    public List<GameObject> partyGhosts = new List<GameObject>();
    public List<Ghost> partyGhostsData = new List<Ghost>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

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

    public void AddGhostToParty(GameObject ghostObject)
    {
        partyGhosts.Add(ghostObject);
    }

    public void RemoveGhostFromParty(GameObject ghostObject)
    {
        Ghost ghostData = ghostObject.GetComponent<Ghost>();
        if (ghostData != null)
        {
            if (partyGhosts.Contains(ghostObject))
            {
                partyGhosts.Remove(ghostObject);
                Debug.Log("Removed ghost from party: " + ghostData.ghostName);
            }
        }
    }
}
