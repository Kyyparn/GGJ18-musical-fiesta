using System.Collections.Generic;
using UnityEngine;

public class EchoMeshSpawner : MonoBehaviour {

    public static List<EchoMeshSpawner> MeshSpawners;
    public GameObject meshSpawnerPrefab;
    private Mesh mesh;
    
	void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        if(MeshSpawners == null)
        {
            MeshSpawners = new List<EchoMeshSpawner>();
        }
        MeshSpawners.Add(this);
    }

    public void CreateCopyOfMesh()
    {
        var gameObject = Instantiate(meshSpawnerPrefab, transform.position, transform.rotation) as GameObject;
        gameObject.transform.localScale = transform.localScale;
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }
}
