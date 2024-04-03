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

    public List<Ghost> partyRosterGhosts = new List<Ghost>(); //This is for the UI
    public List<GameObject> partyGhosts = new List<GameObject>(); //These are the ones that show up in combat


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
        //Debug.Log("Ghosts in Party Roster:");
        foreach (Ghost ghost in partyRosterGhosts)
        {
            //Debug.Log("- " + ghost.ghostName);
        }
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

    public void AddGhostToParty(GameObject ghostObject, Ghost ghost)
    {
        partyGhosts.Add(ghostObject); //for combat
        partyRosterGhosts.Add(ghost); //for UI
    }

    public void RemoveGhostFromParty(GameObject ghostObject, Ghost ghost)
    {
        partyGhosts.Remove(ghostObject); //for combat
        partyRosterGhosts.Remove(ghost); //for UI
    }
}
