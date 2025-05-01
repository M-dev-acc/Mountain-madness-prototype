using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collide");
            dialogueManager.StartDialogue(dialogueData);
        }
    }
}