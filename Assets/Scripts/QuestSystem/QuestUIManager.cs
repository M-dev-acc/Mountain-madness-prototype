using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class QuestUIManager : MonoBehaviour
{
    public GameObject questPanel;                // Parent UI Panel (can toggle visibility)
    public GameObject questEntryPrefab;          // Prefab for each quest UI block
    public QuestManager questManager;            // Reference to QuestManager
    public Transform questListContainer;         // Vertical layout container for all quest entries
    [SerializeField] private GameObject objectiveTextPrefab;

    private Dictionary<QuestInstance, GameObject> activeQuestUI = new();

    void Start()
    {
        HideQuests();
    }

    public void ShowQuests()
    {
        questPanel.SetActive(true);
        RefreshUI();
    }

    public void HideQuests()
    {
        questPanel.SetActive(false);
    }

    public void RefreshUI()
    {
        // Clear old UI
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        activeQuestUI.Clear();

        foreach (var quest in questManager.activeQuests)
        {
            GameObject questUI = Instantiate(questEntryPrefab, questListContainer);
            TextMeshProUGUI[] texts = questUI.GetComponentsInChildren<TextMeshProUGUI>();

            if (texts.Length > 0)
            {
                texts[0].text = quest.questData.title;
                Debug.Log(quest.questData.title);
            }

            Transform objectiveContainer = questUI.transform.Find("Objectives");

            foreach (var obj in quest.questData.objectives)
            {
                GameObject objectiveGO = Instantiate(objectiveTextPrefab, objectiveContainer);
                var text = objectiveGO.GetComponent<TextMeshProUGUI>();

                int progress = quest.progress[obj.itemId];
                text.text = $"{obj.objectiveId}: {progress}/{obj.requiredAmount}";
            }       
            activeQuestUI[quest] = questUI;
        }
    }

    public void UpdateProgressUI()
    {
        foreach (var kvp in activeQuestUI)
        {
            var quest = kvp.Key;
            var questUI = kvp.Value;
            TextMeshProUGUI[] texts = questUI.GetComponentsInChildren<TextMeshProUGUI>();
            if (texts.Length == 0)
                continue;

            TextMeshProUGUI objectiveContainer = texts[1];
            foreach (var obj in quest.questData.objectives)
            {
                int progress = quest.progress[obj.itemId];
                objectiveContainer.text = $"{obj.itemId}: {progress}/{obj.requiredAmount}";
            }
        }
    }
}
