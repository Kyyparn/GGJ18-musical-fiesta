using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class EcoLocationAudioSource : MonoBehaviour
    {
        public GameObject go;
        public float goalLength = 100;
        public float increaseTime = 1.0f;
        public float decreaseTime = 5.0f;

        [ContextMenu("Try sphereswelling")]
        public void PlaySound()
        {
            StartRadiusIncrease();
        }

        void StartRadiusIncrease()
        {
            StartCoroutine(LinearUpdate(0, goalLength, increaseTime, ScaleGo, StartRadiusDecrease));
        }

        void StartRadiusDecrease()
        {
            StartCoroutine(LinearUpdate(goalLength, 0, decreaseTime, ScaleGo));
        }

        void ScaleGo(float value)
        {
            go.transform.localScale = new Vector3(value, value, value);
        }

        IEnumerator LinearUpdate(float from, float to, float endTimeInSeconds, Action<float> func, Action onFinished = null)
        {
            float diff = to - from;
            float time = 0;

            while(time < endTimeInSeconds)
            {
                time += Time.deltaTime;

                if(time > endTimeInSeconds)
                {
                    time = endTimeInSeconds;
                }

                float value = from + diff * (time / endTimeInSeconds);

                func(value);

                yield return new WaitForEndOfFrame();
            }

            if(onFinished != null)
            {
                onFinished();
            }
        }
    }
}
