using UnityEngine;

public class CollideDiveInPowerDown : MonoBehaviour
{
    public CollectibleItemSO diveInPowerDown;
    private void OnTriggerEnter(Collider other)
    {
        diveInPowerDown.ApplyEffect(other.gameObject);
    }
}
