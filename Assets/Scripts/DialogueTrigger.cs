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

    public string HauntingGhost;

    //public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public List<DialogueLine> defaultDialogueLines = new List<DialogueLine>();
    public List<DialogueLine> ghostDefeatedDialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool inRange = false;

    private void Update()
    {
        if (inRange && Input.GetKeyUp(KeyCode.Space))
        {
            TriggerDialogue();
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

    private void TriggerDialogue()
    {
        GameManager.objectNameToDestroy = gameObject.name;

        // Check game state and trigger appropriate dialogue set
        if (GameManager.enemiesToDestroy.Contains(dialogue.HauntingGhost))
        {
            DialogueManager.Instance.StartDialogue(dialogue.ghostDefeatedDialogueLines);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(dialogue.defaultDialogueLines);
            DialogueManager.Instance.SetBattleSceneToLoad(dialogue.battleScene);
        }
    }


    /*
   private void TriggerDialogue()
   {
        GameManager.objectNameToDestroy = gameObject.name;
        // If HauntingGhost name is not in enemiesToDestroy list, load battle scene without triggering dialogue
        DialogueManager.Instance.SetBattleSceneToLoad(dialogue.battleScene);
        DialogueManager.Instance.StartDialogue(dialogue);
   }
    */
}
