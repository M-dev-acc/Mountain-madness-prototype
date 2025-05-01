using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewReward", menuName = "Quest System/Reward")]
public class QuestRewardSO : ScriptableObject
{
    public string skillId;      // e.g., "herbalism"
    public int value;           // Amount to increase
}