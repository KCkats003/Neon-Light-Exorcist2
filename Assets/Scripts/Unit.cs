using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //Enemies name
    public string unitName;

    //How much DMG the enemy will do
    public int damage;

    //Health information
    public int maxHP;
    public int currentHP;

    public static int playerMaxHP;

    private static bool healthInitialized = false;

    void Start()
    {
        if (CompareTag("Player"))
        {
            playerMaxHP = maxHP;

            // Check if player's health has been initialized
            if (!healthInitialized)
            {
                // Set player's health to 50 at the start of the game
                currentHP = 50;
                GameManager.playerHealth = currentHP;

                // Set the flag to true to indicate that health has been initialized
                healthInitialized = true;
            }
            else
            {
                // Player's health has already been initialized, load from PlayerPrefs
                currentHP = PlayerPrefs.GetInt("PlayerHealth", maxHP);
                GameManager.playerHealth = currentHP;
            }
        }
    }

    public bool TakeDamage(int dmg)
    {
        if (CompareTag("Player"))
        {
            currentHP -= dmg;

            if (currentHP <= 0)
            {
                // Save player health to PlayerPrefs
                PlayerPrefs.SetInt("PlayerHealth", maxHP);
                PlayerPrefs.Save();
                return true;
            }
            else
            {
                // Save player health to PlayerPrefs
                PlayerPrefs.SetInt("PlayerHealth", currentHP);
                PlayerPrefs.Save();
                return false;
            }
        }
        else
        {
            // Handle damage for enemies or other units
            currentHP -= dmg;
            if (currentHP <= 0)
            {
                // Handle enemy defeat or unit destruction
            }
            return currentHP <= 0;
        }
    }


    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;

        // Save player health to PlayerPrefs
        PlayerPrefs.SetInt("PlayerHealth", currentHP);
        PlayerPrefs.Save();
    }
}
