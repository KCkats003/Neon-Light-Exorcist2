using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image textBox;
    public Image characterIcon;
    public Image characterIconRight;
    public Image btn;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();

        HideDialogueUI();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        ShowDialogueUI();

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetCanMove(false);
            }
        }


        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;

        if (currentLine.character.useIconRight && currentLine.character.iconRight != null)
        {
            characterIconRight.sprite = currentLine.character.iconRight;
            characterIconRight.gameObject.SetActive(true); 
        }
        else
        {
            characterIconRight.sprite = null;
            characterIconRight.gameObject.SetActive(false);
        }


        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        HideDialogueUI();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetCanMove(true);
            }
        }


    }

    void ShowDialogueUI()
    {
        // Set UI elements to be visible

        characterIcon.gameObject.SetActive(true);
        characterIconRight.gameObject.SetActive(true);
        characterName.gameObject.SetActive(true);
        dialogueArea.gameObject.SetActive(true);
        textBox.gameObject.SetActive(true);
        btn.gameObject.SetActive(true);
    }

    void HideDialogueUI()
    {
        // Set UI elements to be hidden
        characterIcon.gameObject.SetActive(false);
        characterIconRight.gameObject.SetActive(false);
        characterName.gameObject.SetActive(false);
        dialogueArea.gameObject.SetActive(false);
        textBox.gameObject.SetActive(false);
        btn.gameObject.SetActive(false);
    }
}