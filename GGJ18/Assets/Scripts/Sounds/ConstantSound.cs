using Assets.Scripts.Echoes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSound : MonoBehaviour
{
    public float delayBetweenEchos;

    private EcoLocationAudioSource echoAudioSource;
    private float timeUntilNextEcho;

    private void Start()
    {
        echoAudioSource = GetComponent<EcoLocationAudioSource>();
        timeUntilNextEcho = delayBetweenEchos;
    }

    void Update()
    {
        timeUntilNextEcho -= Time.deltaTime;
        if (timeUntilNextEcho < 0)
        {
            echoAudioSource.PlaySonarRing(transform.position);
            timeUntilNextEcho = delayBetweenEchos;
        }
    }
}
