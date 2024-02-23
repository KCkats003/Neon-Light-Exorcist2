using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int playerHealth = 50;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Debug.Log("Player Health: " + playerHealth);
    }
}
