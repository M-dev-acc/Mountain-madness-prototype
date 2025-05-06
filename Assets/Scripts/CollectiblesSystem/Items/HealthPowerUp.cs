using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewHealthPowerUp", menuName = "Collectibles/Add a Health power-up")]
public class HealthPowerUp : CollectibleItemSO
{
    // Start is called before the first frame update
    public float amount;
    public override void ApplyEffect(GameObject collector)
    {
        collector.GetComponent<CharacterMovement>().stamina += amount;
    }
}
