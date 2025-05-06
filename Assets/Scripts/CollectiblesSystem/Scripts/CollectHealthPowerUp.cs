using UnityEngine;

public class CollectHealthPowerUp : MonoBehaviour
{
    public CollectibleItemSO healthPowerUp;
    private void OnTriggerEnter(Collider other)
    {
        healthPowerUp.ApplyEffect(other.gameObject);
        Destroy(gameObject);
    }
}
