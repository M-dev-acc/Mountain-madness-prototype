using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueTree", menuName = "Dialogue System/Create a Dialogue tree")]
public class DialogueData : ScriptableObject
{
    public DialogueNode rootNode;
}
