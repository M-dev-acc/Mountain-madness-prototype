using UnityEngine;

[CreateAssetMenu(fileName ="NewPoisonCollectible", menuName = "Collectibles/Add a Quest 1 item")]
public class SilverBloomQuestItem : CollectibleItemSO
{
    public QuestManager questManager;
    public QuestSO testQuest;
    public QuestUIManager questUIManager;
    public float amount;
    public override void ApplyEffect(GameObject collector)
    {
        questManager.SubmitItem("silver_bloom", 1);
    }
}
