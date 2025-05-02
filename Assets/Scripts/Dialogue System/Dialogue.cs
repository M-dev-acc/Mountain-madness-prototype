using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Tree")]
public class Dialogue : ScriptableObject
{
    public DialogueNode startNode; // The first node in the conversation
}
