using System.Collections.Generic;
using UnityEngine;

public class QuestItemManager : MonoBehaviour
{
    public QuestSO activeQuest; // The currently active quest
    private Dictionary<string, List<GameObject>> questItemsDict; // Cache quest items by tag (itemId)

    void Start()
    {
        // Cache all quest items at the start (only once)
        CacheQuestItems();
    }

    private void CacheQuestItems()
    {
        questItemsDict = new Dictionary<string, List<GameObject>>();

        // Only cache quest items once. For this, make sure your quest items are tagged correctly in the scene.
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("silver_bloom"); // assuming all quest items have the same tag

        foreach (var item in allItems)
        {
            string itemId = item.tag; // Assume tag corresponds to item ID
            if (!questItemsDict.ContainsKey(itemId))
                questItemsDict[itemId] = new List<GameObject>();

            questItemsDict[itemId].Add(item); // Cache the items by their tag
            item.SetActive(false); // Ensure all items are initially hidden
        }
    }

    // Assign a new quest to the player
    public void ShowQuestItems(QuestSO newQuest)
    {
        activeQuest = newQuest;
        SetActiveQuestItems(); // Show items related to this quest's objectives
    }

    // Show quest items that match the item IDs from the quest's objectives
    private void SetActiveQuestItems()
    {
        if (activeQuest == null) return;

        foreach (var objective in activeQuest.objectives)
        {
            if (questItemsDict.ContainsKey(objective.itemId))
            {
                foreach (var item in questItemsDict[objective.itemId])
                {
                    item.SetActive(true); // Show items related to this objective
                }
            }
        }
    }

    // Hide all quest items (in case we have a change of quests or reset)
    private void HideAllQuestItems()
    {
        foreach (var itemsList in questItemsDict.Values)
        {
            foreach (var item in itemsList)
            {
                item.SetActive(false); // Hide item
            }
        }
    }
}
