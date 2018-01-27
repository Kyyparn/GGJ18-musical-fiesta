using Assets.Scripts.Echoes;
using UnityEngine;

public class RepeatingSound : MonoBehaviour
{
    public SoundContainer soundContainer;

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
            var audio = soundContainer.ListOfSounds[Random.Range(0, soundContainer.ListOfSounds.Count)];
            echoAudioSource.PlaySound(audio, transform.position);
            UpdateTimeUntilNextSound();
        }
    }

    private void UpdateTimeUntilNextSound()
    {
        timeUntilNextSound = Random.Range(soundContainer.MinRepeatTime, soundContainer.MaxRepeatTime);
    }
}
