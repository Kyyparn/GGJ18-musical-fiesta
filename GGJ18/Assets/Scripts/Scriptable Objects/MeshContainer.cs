using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MeshContainer : ScriptableObject
{
    public List<Mesh> Meshes;

    public Mesh GetRandomMesh()
    {
        return Meshes[Random.Range(0, Meshes.Count -1)];
    }
}
