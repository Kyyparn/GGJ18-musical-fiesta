using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SceneLoad
{
    public class RelativePoint : MonoBehaviour
    {
        public RelativePointConfiguration configuration;

        void Start()
        {
            if (configuration == RelativePointConfiguration.EntryPoint)
            {
                SceneLoadManager.Instance.RegisterEntryPoint(this);
            }
            else if(configuration == RelativePointConfiguration.EndPoint)
            {
                SceneLoadManager.Instance.RegisterEndPoint(this);
            }
        }
    }
}
