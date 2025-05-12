using UnityEngine;

[CreateAssetMenu(fileName ="NewPoisonCollectible", menuName = "Collectibles/Add a Quest 1 item")]
public class SilverBloomQuestItem : CollectibleItemSO
{
    public float amount;
    public override void ApplyEffect(GameObject collector)
    {
        QuestManager.Instance?.SubmitItem("silver_bloom", 1);
    }
}
