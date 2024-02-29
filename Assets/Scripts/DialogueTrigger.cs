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
        //Debug.Log(gameObject.name);
        DialogueManager.Instance.SetBattleSceneToLoad(dialogue.battleScene);
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
