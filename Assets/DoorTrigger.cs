using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator Door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Door.SetTrigger("OpenDoor");
        }
    }
}

