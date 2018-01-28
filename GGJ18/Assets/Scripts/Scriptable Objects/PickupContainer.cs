using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PickupContainer : ScriptableObject
{
    public List<AudioClip> ThrowSound;
    public List<AudioClip> PickupSound;
}

