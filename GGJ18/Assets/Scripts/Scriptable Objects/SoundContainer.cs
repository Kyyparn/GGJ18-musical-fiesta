using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundContainer : ScriptableObject
{
    public List<AudioClip> ListOfSounds;
    public int MinRepeatTime = 0;
    public int MaxRepeatTime = 0;
}
