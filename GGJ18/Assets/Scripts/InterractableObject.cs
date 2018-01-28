using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractableObject : MonoBehaviour
{
    public Vector3 localPos;
    public Quaternion localRot;
    public Vector3 localScale;

    

    private void Start()
    {
        localPos = transform.localScale;
        localRot = transform.localRotation;
        localScale = transform.localScale;
    }
}
