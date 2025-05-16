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
    private CharacterMovement player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<CharacterMovement>();

            if (intercationCount == 0)
            {
                dialogueManager.StartDialogue(dialogueData);
                player?.SetMovement(false);
            }
            else if (intercationCount > 0)
            {
                QuestInstance activeQuest = questManager.GetActiveQuestBySO(testQuest);
                if (activeQuest.IsCompleted)
                {
                    player?.SetMovement(false);
                    activeQuest.GetRewards()[0].ApplyEffect(other.gameObject);
                    Debug.Log(HealthManager.Instance.staminaDrain);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.gameObject.GetComponent<CharacterMovement>();
            }

            if (!dialogueManager.IsDialogueRunning())
            {
                player?.SetMovement(true);

                if (!questManager.isQuestAssigned(testQuest))
                {
                    questManager?.AssignQuest(testQuest);
                    questUIManager?.ShowQuests();
                    questItemManager?.ShowQuestItems(testQuest);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        intercationCount++;
    }
}