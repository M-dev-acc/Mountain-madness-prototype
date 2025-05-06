using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData;

    [SerializeField] private QuestManager questManager;
    [SerializeField] private QuestSO testQuest;
    [SerializeField] private QuestUIManager questUIManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other);
            dialogueManager.StartDialogue(dialogueData);
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
            player.SetMovement(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
            // player.setMovement(false);
            if (!dialogueManager.IsDialogueRunning())
            {
                player?.SetMovement(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!questManager.isQuestAssigned(testQuest))
        {
            questManager?.AssignQuest(testQuest);
            questUIManager?.ShowQuests();
        }
    }
}