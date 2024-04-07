using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Enemies name
    public string unitName;

    // How much DMG the enemy will do
    public int damage;

    // Health information
    public int maxHP;
    public int currentHP;

    // What type the ghost is
    public enum GhostType { Red, Blue, Green }
    public GhostType type;

    private void Start()
    {
        if (CompareTag("Player"))
        {
            // Initialize player's health
            currentHP = GameManager.playerHealth;
        }
    }


    // Function to take damage
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        return false;
    }

    // Function to heal
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
