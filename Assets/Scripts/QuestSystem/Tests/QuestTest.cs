using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest : MonoBehaviour
{
    public QuestManager questManager;
    public QuestSO testQuest;

    void Start()
    {
        questManager.AssignQuest(testQuest);
        questManager.PrintProgress();

        Debug.Log("Simulating item collection...");
        questManager.SubmitItem("silver_bloom", 1);
        questManager.PrintProgress();

        questManager.SubmitItem("silver_bloom", 1);
        questManager.PrintProgress();

        questManager.SubmitItem("silver_bloom", 1);
        questManager.PrintProgress();
    }
}