﻿using Assets.Scripts.Echoes;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public GameObject key;
    public PickupContainer lockSounds;
    public Elevator Elevator;

    private List<AudioClip> lockedSounds;
    private List<AudioClip> unlockedSounds;

    private EcoLocationAudioSource ecoLocationAudioSource;

    private void Start()
    {
        lockedSounds = lockSounds.ThrowSound;
        unlockedSounds = lockSounds.PickupSound;
        ecoLocationAudioSource = GetComponent<EcoLocationAudioSource>();
    }

    public override void Interract()
    {
        var player = GameManager.Instance.Player;
        if (player.holdingObject && player.currentObjectInHands == key)
        {
            UnlockSound();
            player.OpenLock();
            //Elevator.OpenDoors();
            Destroy(this.gameObject,0.25f);
        }
        else
        {
            LockedSound();
        }
    }

    public void UnlockSound()
    {
        var audio = unlockedSounds[Random.Range(0, unlockedSounds.Count)];
        ecoLocationAudioSource.PlaySound(audio, transform.position);
    }

    public void LockedSound()
    {
        if(! GetComponent<AudioSource>().isPlaying)
        {
            var audio = lockedSounds[Random.Range(0, lockedSounds.Count)];
            ecoLocationAudioSource.PlaySound(audio, transform.position);
        }
    }

}
