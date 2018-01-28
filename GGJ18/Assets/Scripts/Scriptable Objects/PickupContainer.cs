using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PickupContainer : ScriptableObject
{
    public List<AudioClip> ThrowSound;
    public float throwSoundIntensity;
    public List<AudioClip> PickupSound;
    public float pickUpSoundIntensity;

    public bool isObjective = false;
}

