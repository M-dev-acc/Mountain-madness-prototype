using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestInstance> activeQuests = new List<QuestInstance>();
    public QuestUIManager questUIManager;

    public void AssignQuest(QuestSO quest)
    {
        if (activeQuests.Exists(q => q.questData == quest)) return;
        var questInstance = new QuestInstance(quest);
        activeQuests.Add(questInstance);
        // Debug.Log($"Assigned quest: {quest.title}");
    }

    public void SubmitItem(string itemId, int amount)
    {
        foreach (var quest in activeQuests)
        {
            quest.UpdateProgress(itemId, amount);
            if (quest.IsCompleted)
            {
                Debug.Log($"Quest completed: {quest.questData.title}");
                // TODO: Add additional logic
            }
        }

        questUIManager?.UpdateProgressUI();
    }

    public void PrintProgress()
    {
        foreach (var quest in activeQuests)
        {
            Debug.Log($"Quest: {quest.questData.title}");
            foreach (var obj in quest.questData.objectives)
            {
                int progress = quest.progress[obj.itemId];
                Debug.Log($"  {obj.itemId}: {progress}/{obj.requiredAmount}");
            }
        }
    }

    public bool isQuestAssigned(QuestSO quest)
    {
        bool isQuestAssigned = activeQuests.Exists(q => q.questData == quest) ? true : false;
        
        return isQuestAssigned;
    }
}
