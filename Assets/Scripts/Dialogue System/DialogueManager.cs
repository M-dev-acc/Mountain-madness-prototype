using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Dialogue currentDialogue;  // The active dialogue
    private DialogueNode currentNode; // Current node being displayed

    [Header("UI Elements")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public GameObject choicePanel;
    public GameObject choiceButtonPrefab;
    public Transform choiceContainer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentNode = dialogue.startNode;
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        if (currentNode == null) return;

        speakerText.text = currentNode.speakerName;
        dialogueText.text = currentNode.text;

        // Remove old choices
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }

        // Display choices if there are follow-ups
        if (currentNode.nextNodes.Count > 0)
        {
            choicePanel.SetActive(true);
            foreach (DialogueNode nextNode in currentNode.nextNodes)
            {
                GameObject button = Instantiate(choiceButtonPrefab, choiceContainer);
                button.GetComponentInChildren<TextMeshProUGUI>().text = nextNode.text;
                button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => AdvanceDialogue(nextNode));
            }
        }
        else
        {
            choicePanel.SetActive(false);
        }
    }

    public void AdvanceDialogue(DialogueNode nextNode)
    {
        currentNode = nextNode;
        ShowDialogue();
    }
}
