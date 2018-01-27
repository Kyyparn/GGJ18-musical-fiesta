using Assets.Scripts.Echoes;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingSound : MonoBehaviour
{
    public List<AudioClip> sounds;
    public float timeBetweenSounds;
    public float maxRandomExtraDelay;

    private EcoLocationAudioSource echoAudioSource;
    private float timeUntilNextSound;

    private void Start()
    {
        echoAudioSource = GetComponent<EcoLocationAudioSource>();
        UpdateTimeUntilNextSound();
    }

    void Update()
    {
        timeUntilNextSound -= Time.deltaTime;
        if(timeUntilNextSound < 0)
        {
            var audio = sounds[Random.Range(0, sounds.Count)];
            echoAudioSource.PlaySound(audio);
            UpdateTimeUntilNextSound();
        }
    }

    private void UpdateTimeUntilNextSound()
    {
        timeUntilNextSound = timeBetweenSounds + Random.Range(0, maxRandomExtraDelay);
    }
}
