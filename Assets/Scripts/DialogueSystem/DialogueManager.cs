using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUIManager dialogueUI;

    private DialogueData currentDialogue;
    private DialogueNode currentNode;

    public bool IsRunning{ get; private set; } = false;

    // Externally call this to start a dialogue
    public void StartDialogue(DialogueData dialogueData)
    {
        if (dialogueData == null || dialogueData.rootNode == null)
        {
            Debug.LogError("Dialogue Data or Root Node is not assigned!");
            return;
        }

        currentDialogue = dialogueData;
        currentNode = currentDialogue.rootNode;
        IsRunning = true;

        ShowCurrentNode();
    }

    private void ShowCurrentNode()
    {
        if (!IsRunning || currentNode == null)
        {
            Debug.LogWarning("Dialogue not running or no current node.");
            return;
        }

        List<string> choiceTexts = new List<string>();
        bool isLastDialogue = false;

        if (currentNode.dialogueChoices.Count > 0)
        {
            foreach (DialogueChoice choice in currentNode.dialogueChoices)
            {
                choiceTexts.Add(choice.choiceText);
            }
        }
        else
        {
            isLastDialogue = true;
        }

        dialogueUI.ShowDialogue(
            currentNode.speakerName,
            currentNode.dialogueText,
            choiceTexts,
            OnChoiceSelected,
            isLastDialogue
        );
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        if (choiceIndex < 0 || choiceIndex >= currentNode.dialogueChoices.Count)
        {
            Debug.LogError("Invalid choice index");
            return;
        }

        DialogueChoice selectedChoice = currentNode.dialogueChoices[choiceIndex];

        if (selectedChoice.nextNode != null)
        {
            currentNode = selectedChoice.nextNode;
            ShowCurrentNode();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueUI.HideDialogue();
        IsRunning = false;
        currentNode = null;
        currentDialogue = null;
    }

    // Optional: expose running state
    public bool IsDialogueRunning()
    {
        return IsRunning;
    }
}
