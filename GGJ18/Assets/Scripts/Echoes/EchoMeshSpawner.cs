using System.Collections.Generic;
using UnityEngine;

public class EchoMeshSpawner : MonoBehaviour {

    private static List<EchoMeshSpawner> meshSpawners;
    public static List<EchoMeshSpawner> MeshSpawners
    {
        get
        {
            if (meshSpawners == null)
            {
                meshSpawners = new List<EchoMeshSpawner>();
            }
            return meshSpawners;
        }
        set { }
    }
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

    private void OnDestroy()
    {
        MeshSpawners.Remove(this);
    }

    public void CreateCopyOfMesh()
    {
        var gameObject = Instantiate(meshSpawnerPrefab, transform.position, transform.rotation) as GameObject;
        gameObject.transform.localScale = transform.localScale;
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }
}
