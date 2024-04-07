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
public class ActScenes
{
    public string actSceneOne;
    public string actSceneTwo;
    public string actSceneThree;
}

[System.Serializable]
public class Dialogue
{
    public string battleScene;

    public string HauntingGhost;

    //public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public List<DialogueLine> defaultDialogueLines = new List<DialogueLine>();
    public List<DialogueLine> ghostDefeatedDialogueLines = new List<DialogueLine>();
    public List<DialogueLine> finalDialogueLines = new List<DialogueLine>();
    
    //Triggers final dialogue
    public bool finalDialogue;
}

public class DialogueTrigger : MonoBehaviour
{
    public ActScenes actScenes;
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

        if (dialogue.finalDialogue == true)
        {
            DialogueManager.Instance.StartDialogue(dialogue.finalDialogueLines);
           

        }
        else if (GameManager.enemiesToDestroy.Contains(dialogue.HauntingGhost))
        {
            DialogueManager.Instance.StartDialogue(dialogue.ghostDefeatedDialogueLines);
            dialogue.finalDialogue = true;

     
        }
        else
        {
            DialogueManager.Instance.StartDialogue(dialogue.defaultDialogueLines);


                if (GameManager.actI == true && dialogue.battleScene == "")
                {
                DialogueManager.Instance.SetBattleSceneToLoad(actScenes.actSceneOne);
                }
                else if (GameManager.actII == true && dialogue.battleScene == "")
                {
                    DialogueManager.Instance.SetBattleSceneToLoad(actScenes.actSceneTwo);
                }
                else if (GameManager.actIII == true && dialogue.battleScene == "")
                {
                    DialogueManager.Instance.SetBattleSceneToLoad(actScenes.actSceneThree);
                } else {
                DialogueManager.Instance.SetBattleSceneToLoad(dialogue.battleScene);
                
                }

            if (dialogue.HauntingGhost != "") { 
            GameManager.instance.ShowGhost(dialogue.HauntingGhost);
            }

            /*
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (GameManager.actI = true && player.CompareTag("scientist"))
            {
                DialogueManager.Instance.SetBattleSceneToLoad(dialogue.nextActScene);
            }
            else if (GameManager.actII = true && player.CompareTag("scientist"))
            {
                DialogueManager.Instance.SetBattleSceneToLoad(dialogue.nextActScene);
            } else if (GameManager.actIII = true && player.CompareTag("scientist"))
            {
                DialogueManager.Instance.SetBattleSceneToLoad(dialogue.nextActScene);
            }
            */


        }
    }
}
