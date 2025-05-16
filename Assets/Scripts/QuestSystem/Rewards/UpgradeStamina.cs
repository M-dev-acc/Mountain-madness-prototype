using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

[CreateAssetMenu(fileName ="NewUpgradeStaminaReward", menuName = "Quest System/Add a Upgrade Stamina reward")]
public class UpgradeStamina : QuestRewardSO
{
    public float rewardAmount;
    public override void ApplyEffect(GameObject collector)
    {
        HealthManager.Instance.staminaDrain = rewardAmount;
    }
}
