using UnityEngine;

public class CollectPoisonPowerDown : MonoBehaviour
{
    public CollectibleItemSO poisonCollectible;
    private void OnTriggerEnter(Collider other)
    {
        poisonCollectible.ApplyEffect(other.gameObject);
    }
}
