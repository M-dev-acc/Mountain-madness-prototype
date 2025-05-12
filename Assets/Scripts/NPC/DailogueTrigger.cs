using UnityEngine;

public class DailogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData;

    [SerializeField] private QuestManager questManager;
    [SerializeField] private QuestSO testQuest;
    [SerializeField] private QuestUIManager questUIManager;
    [SerializeField] private QuestItemManager questItemManager;

    private int intercationCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && intercationCount == 0)
        {
            dialogueManager.StartDialogue(dialogueData);
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
            player?.SetMovement(false);
            intercationCount++;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
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
            questItemManager?.ShowQuestItems(testQuest);
        }
    }
}