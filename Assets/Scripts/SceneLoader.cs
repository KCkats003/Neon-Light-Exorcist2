using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class SceneLoader : MonoBehaviour
{


    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;
    public Slider hpSlider;

    void Start()
    {

        currentHealthText.text = GameManager.playerHealth.ToString();

        hpSlider.value = GameManager.playerHealth;
        maxHealthText.text = "100";

        foreach (string enemyName in GameManager.enemiesToDestroy)
        {
            GameObject enemyToDestroy = GameObject.Find(enemyName);
            if (enemyToDestroy != null)
            {
                Destroy(enemyToDestroy);
            }
        }

        // Now that all enemies are destroyed, update scene flags based on enemy count
        if (GameManager.enemiesToDestroy.Count == 1)
        {
            GameManager.actI = true;
        }
        else if (GameManager.enemiesToDestroy.Count == 3)
        {
            GameManager.actI = false;
            GameManager.actII = true;
        }
        else if (GameManager.enemiesToDestroy.Count == 5)
        {
            GameManager.actII = false;
            GameManager.actIII = true;
        }
        else if (GameManager.enemiesToDestroy.Count == 7){
            
            GameManager.actIII = false;
            GameManager.finalBattle = true;
        }

        Debug.Log(GameManager.enemiesToDestroy.Count);
        Debug.Log("SCENE 1 is: " + GameManager.actI);
        Debug.Log("SCENE 2 is: " + GameManager.actII);
        Debug.Log("SCENE 3 is: " + GameManager.actIII);
    }

    public void updateHealth()
    {
        currentHealthText.text = GameManager.playerHealth.ToString();

        hpSlider.value = GameManager.playerHealth;
        maxHealthText.text = "100";
    }
}
