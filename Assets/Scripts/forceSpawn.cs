using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceSpawn : MonoBehaviour
{
    // The position where you want the player to spawn
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    // This method is called before the first frame update
    void Start()
    {
        // Set the player's position to the spawn position
        transform.position = spawnPosition;
    }
}
