using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
    public Sprite iconRight;
    public bool useIconRight;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public string battleScene;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Save player's position before triggering dialogue
            SavePlayerPosition();

            // Trigger dialogue
            TriggerDialogue();
        }
    }

    private void SavePlayerPosition()
    {
        // Save player's position using PlayerPrefs or other persistent storage
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);
        PlayerPrefs.Save();
    }

    private void TriggerDialogue()
    {
        DialogueManager.Instance.SetBattleSceneToLoad(dialogue.battleScene);
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
