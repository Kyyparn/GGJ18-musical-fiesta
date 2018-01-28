using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Characters;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Characters.Player
{
    public class Player : Character
    {
        public bool isAlive = true;

        public bool holdingObject = false;
        public GameObject currentObjectInHands { get; private set; }

        public Transform pickupPosition;

        void Start()
        {
            GameManager.Instance.Player = this;
        }

        public void SwapObjects(GameObject go)
        {
            pickupPosition.DetachChildren();

            var objectStats = currentObjectInHands.GetComponent<InterractableObject>();

            currentObjectInHands.transform.localPosition = objectStats.localPos;
            currentObjectInHands.transform.localRotation = objectStats.localRot;
            currentObjectInHands.transform.localScale = objectStats.localScale;

            var pos = go.transform.position;
            var rot = go.transform.rotation;
            currentObjectInHands.transform.position = pos;
            currentObjectInHands.transform.rotation = rot;

            

            

            
            PickupObject(go);
        }

        public void PickupObject(GameObject go)
        {
            go.transform.parent = pickupPosition;
            
            go.transform.localPosition = new Vector3();
            go.transform.localRotation = new Quaternion();
            go.transform.localScale = new Vector3(1, 1, 1);
            currentObjectInHands = go;
            holdingObject = true;
        }
    }
}
