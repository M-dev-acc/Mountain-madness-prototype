using UnityEngine;

[CreateAssetMenu(fileName = "NewObjective", menuName = "Quest System/Create a Objective")]
public class QuestObjectiveSO : ScriptableObject
{
    public string objectiveId;       // e.g., "collect_red_herb"
    public string itemId;            // Which item to track
    public int requiredAmount = 1;
}
