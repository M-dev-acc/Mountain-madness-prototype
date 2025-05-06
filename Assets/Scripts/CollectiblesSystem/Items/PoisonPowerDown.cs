using UnityEngine;

[CreateAssetMenu(fileName ="NewPoisonCollectible", menuName = "Collectibles/Add a Poison power-down")]
public class PoisonPowerDown : CollectibleItemSO
{
    // Start is called before the first frame update
    public float amount;
    public override void ApplyEffect(GameObject collector)
    {
        collector.GetComponent<CharacterMovement>().stamina -= amount;
    }
}
