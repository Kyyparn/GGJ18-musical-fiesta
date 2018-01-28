using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool IsElevatorOpen;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if(IsElevatorOpen)
        {
            animator.SetBool("OpenDoor", true);
        }
        else
        {
            animator.SetBool("CloseDoor", true);
        }
    }

    public void OpenDoors()
    {
        if (! IsElevatorOpen)
        {
            animator.SetBool("CloseDoor", false);
            animator.SetBool("OpenDoor", true);
            IsElevatorOpen = true;
        }
    }

    public void CloseDoors()
    {
        if (IsElevatorOpen)
        {
            animator.SetBool("CloseDoor", true);
            animator.SetBool("OpenDoor", false);
            IsElevatorOpen = false;
        }
    }
}
