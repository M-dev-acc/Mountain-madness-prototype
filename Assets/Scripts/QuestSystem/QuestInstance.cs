using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInstance
{
    public QuestSO questData;
    public Dictionary<string, int> progress = new Dictionary<string, int>();

    public bool IsCompleted => CheckCompletion();

    public QuestInstance(QuestSO quest)
    {
        questData = quest;
        foreach (var obj in quest.objectives)
            progress[obj.itemId] = 0;
    }

    public void UpdateProgress(string itemId, int amount)
    {
        if (!progress.ContainsKey(itemId)) return;

        progress[itemId] += amount;
        if (progress[itemId] > GetRequiredAmount(itemId))
            progress[itemId] = GetRequiredAmount(itemId);
    }

    public bool CheckCompletion()
    {
        foreach (var obj in questData.objectives)
        {
            if (progress[obj.itemId] < obj.requiredAmount)
                return false;
        }
        return true;
    }

    private int GetRequiredAmount(string itemId)
    {
        foreach (var obj in questData.objectives)
            if (obj.itemId == itemId) return obj.requiredAmount;
        return 0;
    }
}

