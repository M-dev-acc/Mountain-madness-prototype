using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionLevelManager : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData;

    // [SerializeField] private QuestManager questManager;
    // [SerializeField] private QuestUIManager questUIManager;
    // [SerializeField] private QuestItemManager questItemManager;
    // [SerializeField] private QuestSO tutorialQuest;

    private CharacterMovement player = CharacterMovement.Instance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script is set!!!");
        dialogueManager.StartDialogue(dialogueData);
        // player?.SetMovement(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
