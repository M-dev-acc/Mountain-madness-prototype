using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleItemSO : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target);
}
