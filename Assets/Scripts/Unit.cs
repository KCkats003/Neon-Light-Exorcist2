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

    //What type the ghost is
    public enum GhostType { Red, Blue, Green }
    public GhostType type;

    public static int playerMaxHP;

    private static bool healthInitialized = false;

    void Start()
    {
        if (CompareTag("Player"))
        {
            playerMaxHP = maxHP;

            if (!healthInitialized)
            {

                currentHP = 50;
                GameManager.playerHealth = currentHP;

                healthInitialized = true;
            }
            else
            {
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
                PlayerPrefs.SetInt("PlayerHealth", maxHP);
                PlayerPrefs.Save();
                return true;
            }
            else
            {
                PlayerPrefs.SetInt("PlayerHealth", currentHP);
                PlayerPrefs.Save();
                return false;
            }
        }
        else
        {
            currentHP -= dmg;
            if (currentHP <= 0)
            {
            }
            return currentHP <= 0;
        }
    }


    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;

        PlayerPrefs.SetInt("PlayerHealth", currentHP);
        PlayerPrefs.Save();
    }
}
