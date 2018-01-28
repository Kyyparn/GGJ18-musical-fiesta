using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{  
    public Vector3 localPos;
    public Quaternion localRot;
    public Vector3 localScale;

    public PickupContainer pickupData;

    private List<AudioClip> pickupSoundList;
    private List<AudioClip> throwSoundList;

	// Use this for initialization
	void Start ()
    {
        localPos = transform.localScale;
        localRot = transform.localRotation;
        localScale = transform.localScale;

        pickupSoundList = pickupData.PickupSound;
        throwSoundList = pickupData.ThrowSound;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    public override void Interract()
    {
        Debug.Log("Interract with " + this.gameObject.name);
        var player = GameManager.Instance.Player;
        if (player.holdingObject)
        {
            player.SwapObjects(this.gameObject);
            PickupSound();
        }
        else
        {
            player.PickupObject(this.gameObject);
            PickupSound();
        }
    }

    public override void Throw()
    {
        var player = GameManager.Instance.Player;

    }

    public void PickupSound()
    {
        var audio = pickupSoundList[Random.Range(0, pickupSoundList.Count)];
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();
    }
}
