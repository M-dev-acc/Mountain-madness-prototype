using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Create a Dialogue")]
public class DialogueNode:ScriptableObject
{
    public string speakerName;
    [TextArea(2, 5)] public string dialogueText;
    public List<DialogueChoice> dialogueChoices;
}
