using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vendingMachine : MonoBehaviour
{
    private bool inRange = false;
    private bool hasBeenUsed = false; // Track whether the vending machine has been used

    private void Update()
    {
        if (inRange && !hasBeenUsed && Input.GetKeyDown(KeyCode.Space))
        {
            IncreasePlayerHealth();
            hasBeenUsed = true; // Set to true after the vending machine is used
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void IncreasePlayerHealth()
    {
        GameManager.playerHealth += 10;
        Debug.Log("Player Health increased to: " + GameManager.playerHealth);

        PlayerPrefs.SetInt("PlayerHealth", GameManager.playerHealth);
        PlayerPrefs.Save();
    }
}
