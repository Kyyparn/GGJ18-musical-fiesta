using UnityEngine;

public class MonsterEchoMeshSpawner : EchoMeshSpawner
{
    public MeshContainer PatrolMeshes;
    public MeshContainer ChaseMeshes;

    public MonsterState MonsterState;

    public override void CreateCopyOfMesh(Vector3 aroundPoint, float radius)
    {
        base.CreateCopyOfMesh(aroundPoint, radius);
        if(MonsterState == MonsterState.Patrol)
        {
            meshFilter.mesh = PatrolMeshes.GetRandomMesh();
        }
        else
        {
            meshFilter.mesh = ChaseMeshes.GetRandomMesh();
        }
    }
}
