using UnityEngine;

[CreateAssetMenu(fileName = "NewReward", menuName = "Quest System/Create a Reward")]
public class QuestRewardSO : ScriptableObject
{
    public string skillId;      // e.g., "herbalism"
    public int value;           // Amount to increase
}