using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLab : MonoBehaviour
{
    public string labEnterScene;
    private bool inRange;

    private Vector3 playerEnterPosition; // Store the position where player enters

    private void Update()
    {
        if (inRange && Input.GetKeyUp(KeyCode.Space))
        {
            // Save the player's enter position in the GameManager
            GameManager.playerStartPosition = playerEnterPosition;
            SceneManager.LoadScene(labEnterScene, LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("In zone");
        inRange = true;

        // Capture the player's position upon entering the trigger zone
        if (other.CompareTag("Player"))
        {
            playerEnterPosition = other.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}

/*
 * 
 * 
 *     void EndDialogue()
    {
        isDialogueActive = false;
        HideDialogueUI();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerSpawnPosition = player.transform.position;
            //PlayerController playerController = player.GetComponent<PlayerController>();
            playerSpawnPosition = player.transform.position;
            PlayerController_Katie playerController = player.GetComponent<PlayerController_Katie>();
            if (playerController != null)
            {
                playerController.SetCanMove(true);
            }
        }

        if (!string.IsNullOrEmpty(battleSceneToLoad))
        {

            GameManager.playerStartPosition = playerSpawnPosition;

            Debug.Log("Location saved at: " + playerSpawnPosition);

            SceneManager.LoadScene(battleSceneToLoad, LoadSceneMode.Single);
        }

    }
 * 
 * 
 */