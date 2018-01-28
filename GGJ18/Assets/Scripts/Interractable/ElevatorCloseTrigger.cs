using Assets.Scripts.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCloseTrigger : MonoBehaviour {

    public Elevator elevator;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if (player)
        {
            elevator.CloseDoors();
        }
    }
}
