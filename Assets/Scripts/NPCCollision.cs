using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)  // For physics-based collision
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("NPC collided with the Player!");
        }
    }

    void OnTriggerEnter(Collider other)  // For trigger-based collision
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC entered the Player's area!");
        }
    }
}
