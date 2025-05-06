using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Create a Quest")]
public class QuestSO : ScriptableObject
{
    public string questId;
    public string title;
    [TextArea] public string description;

    public QuestObjectiveSO[] objectives;
    public QuestRewardSO[] rewards;
}