using UnityEngine;

[CreateAssetMenu(fileName ="NewPoisonCollectible", menuName = "Collectibles/Add a Dive in power-down")]
public class DiveInPowerDown : CollectibleItemSO
{
    // Start is called before the first frame update
    public float amount;
    public override void ApplyEffect(GameObject collector)
    {
        collector.GetComponent<CharacterMovement>().stamina *= amount;
    }
}
