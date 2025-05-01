using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class QuestSO : ScriptableObject
{
    public string questId;
    public string title;
    [TextArea] public string description;

    public QuestObjectiveSO[] objectives;
    public QuestRewardSO[] rewards;
}