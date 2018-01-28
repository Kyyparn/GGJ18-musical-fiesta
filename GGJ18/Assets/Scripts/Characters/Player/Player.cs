using UnityEngine;
using Assets.Scripts.Managers;
using System;
using Assets.Scripts.Echoes;

namespace Assets.Scripts.Characters.Player
{
    public class Player : Character
    {
        public bool isAlive = true;

        public bool holdingObject = false;
        public GameObject currentObjectInHands { get; private set; }

        public Transform pickupPosition;

        [Header("Scream")]
        public SoundContainer screamSounds;
        public EcoLocationAudioSource scream;
        public float screamCooldown = 2.0f;
        private DateTime lastScream; 

        void Start()
        {
            GameManager.Instance.Player = this;
            lastScream = DateTime.MinValue;
        }

        public void Scream()
        {
            if ((DateTime.Now - lastScream).TotalSeconds >= screamCooldown)
            {
                lastScream = DateTime.Now;

                var audio = screamSounds.ListOfSounds[UnityEngine.Random.Range(0, screamSounds.ListOfSounds.Count)];
                scream.PlaySound(audio, transform.position);
            }
        }

        public void SwapObjects(GameObject go)
        {
            pickupPosition.DetachChildren();

            var objectStats = currentObjectInHands.GetComponent<PickupItem>();

            currentObjectInHands.transform.localPosition = objectStats.localPos;
            currentObjectInHands.transform.localRotation = objectStats.localRot;
            currentObjectInHands.transform.localScale = objectStats.localScale;

            currentObjectInHands.GetComponent<EchoMeshSpawner>().enabled = true;
            currentObjectInHands.GetComponent<MeshRenderer>().enabled = false;

            var pos = go.transform.position;
            var rot = go.transform.rotation;
            currentObjectInHands.transform.position = pos;
            currentObjectInHands.transform.rotation = rot;
            currentObjectInHands.GetComponent<Rigidbody>().isKinematic = true;
            SetCollidersEnabled(currentObjectInHands, true);
            PickupObject(go);
        }

        public void PickupObject(GameObject go)
        {
            go.transform.parent = pickupPosition;
            
            go.transform.localPosition = new Vector3();
            go.transform.localRotation = new Quaternion();
            go.transform.localScale = go.transform.localScale;
            currentObjectInHands = go;
            holdingObject = true;
            SetCollidersEnabled(go, false);
            go.GetComponent<Rigidbody>().isKinematic = true;
        }

        public void ThrowObject()
        {
            pickupPosition.DetachChildren();
            var rigidBody = currentObjectInHands.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.AddForce(Camera.main.gameObject.transform.forward * 500);
            holdingObject = false;
            SetCollidersEnabled(currentObjectInHands, true);
            currentObjectInHands.GetComponent<EchoMeshSpawner>().enabled = true;
            currentObjectInHands.GetComponent<MeshRenderer>().enabled = false;
            currentObjectInHands = null;
        }

        public void SetCollidersEnabled(GameObject gameObject, bool value)
        {
            var colliders = gameObject.GetComponents<BoxCollider>();
            foreach (var collider in colliders)
            {
                collider.enabled = value;
            }
        }

        public void OpenLock()
        {
            pickupPosition.DetachChildren();
            Destroy(currentObjectInHands);
            currentObjectInHands = null;
            holdingObject = false;
        }
    }
}
