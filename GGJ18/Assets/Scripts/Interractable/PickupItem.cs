using Assets.Scripts.Echoes;
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
    private EcoLocationAudioSource ecoLocationAudioSource;

	// Use this for initialization
	void Start ()
    {
        localPos = transform.localScale;
        localRot = transform.localRotation;
        localScale = transform.localScale;

        pickupSoundList = pickupData.PickupSound;
        throwSoundList = pickupData.ThrowSound;

        ecoLocationAudioSource = GetComponent<EcoLocationAudioSource>();
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
            PickupSound();
            player.SwapObjects(this.gameObject);
        }
        else
        {
            PickupSound();
            player.PickupObject(this.gameObject);
        }
    }

    public override void Throw()
    {
        var player = GameManager.Instance.Player;

    }

    public void PickupSound()
    {
        var audio = pickupSoundList[Random.Range(0, pickupSoundList.Count)];
        ecoLocationAudioSource.intensity = pickupData.pickUpSoundIntensity;
        ecoLocationAudioSource.PlaySound(audio, transform.position);
    }

    void OnCollisionEnter()
    {
        var audio = throwSoundList[Random.Range(0, throwSoundList.Count)];
        ecoLocationAudioSource.intensity = pickupData.throwSoundIntensity;
        ecoLocationAudioSource.PlaySound(audio, transform.position);
    }
}
