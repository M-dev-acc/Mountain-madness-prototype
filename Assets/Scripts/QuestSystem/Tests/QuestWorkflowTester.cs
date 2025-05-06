using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWorkflowTester : MonoBehaviour
{
    public QuestManager questManager;
    public QuestSO testQuest;
    public QuestUIManager questUIManager;

    private bool hasAssigned = false;

    void Start()
    {
        if (questManager == null || testQuest == null)
        {
            Debug.LogError("QuestManager or testQuest is not assigned.");
            return;
        }

        // Assign test quest at runtime
        questManager.AssignQuest(testQuest);
        Debug.Log("Assigned Quest: " + testQuest.title);
        questUIManager?.ShowQuests();
        hasAssigned = true;
    }

    void Update()
    {
        if (!hasAssigned) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SubmitItem("silver_bloom", 1); // Partial
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SubmitItem("sliver_bloom", 1); // Finish red herb
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SubmitItem("silver_bloom", 1); // Finish quest
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            questUIManager?.ShowQuests();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            questUIManager?.HideQuests();
        }
    }

    void SubmitItem(string itemId, int amount)
    {
        Debug.Log($"Submitting {amount} x {itemId}");
        questManager.SubmitItem(itemId, amount);
    }
}

