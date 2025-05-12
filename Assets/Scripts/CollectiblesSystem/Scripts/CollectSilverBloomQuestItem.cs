using UnityEngine;

public class CollectSilverBloomQuestItem : MonoBehaviour
{
    public CollectibleItemSO silverBloomQuestItem;
    private void OnTriggerEnter(Collider other)
    {
        silverBloomQuestItem.ApplyEffect(other.gameObject);
        Destroy(gameObject);
    }
}
