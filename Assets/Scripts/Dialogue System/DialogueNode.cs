using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    public string speakerName;         // Name of the speaker (NPC or Player)
    [TextArea(3, 5)] public string text; // The dialogue text
    public List<DialogueNode> nextNodes; // Next possible dialogues (for choices)
}
