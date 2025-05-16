using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    public List<QuestInstance> activeQuests = new List<QuestInstance>();
    public QuestUIManager questUIManager;
    public event Action<QuestSO> onQuestAssigned;

    void Awake()
    {
        Instance = this;
    }

    public void AssignQuest(QuestSO quest)
    {
        if (activeQuests.Exists(q => q.questData == quest)) return;
        var questInstance = new QuestInstance(quest);
        activeQuests.Add(questInstance);
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

    public QuestInstance GetActiveQuestBySO(QuestSO questSO)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.questData == questSO)
                return quest;
        }
        return null;
    }

    public void ResetQuest(string itemId)
    {
        foreach (var quest in activeQuests)
        {
            quest.ResetProgress(itemId);
        }

        questUIManager?.UpdateProgressUI();
    }
}
