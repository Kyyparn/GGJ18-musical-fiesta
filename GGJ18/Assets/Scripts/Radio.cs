using Assets.Scripts.Echoes;
using UnityEngine;

public class Radio : Interactable
{
    public AudioClip StaticRadio;
    public float StaticRadioIntensity;
    public float StaticRadioVolume;
    public AudioClip ChurchillSpeech;
    public float ChurchillSpeechIntensity;
    public float ChurchillSpeechVolume;

    private RadioState state;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            SwitchToStatic();
        }
    }

    public override void Interract()
    {
        if (state == RadioState.Static)
        {
            SwitchToChurchill();
        }
    }

    public void SwitchToChurchill()
    {
        audioSource.clip = ChurchillSpeech;
        audioSource.loop = false;
        audioSource.volume = ChurchillSpeechVolume;
        var echoAudio = GetComponent<EcoLocationAudioSource>();
        echoAudio.intensity = ChurchillSpeechIntensity;
        echoAudio.isAmbientSound = false;
        audioSource.Play();
    }

    public void SwitchToStatic()
    {
        audioSource.clip = StaticRadio;
        audioSource.loop = true;
        audioSource.volume = StaticRadioVolume;
        var echoAudio = GetComponent<EcoLocationAudioSource>();
        echoAudio.intensity = StaticRadioIntensity;
        echoAudio.isAmbientSound = true;
        audioSource.Play();
    }
}
