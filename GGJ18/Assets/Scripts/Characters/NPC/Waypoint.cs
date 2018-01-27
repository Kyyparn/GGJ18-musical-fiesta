using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public Transform[] possiblePaths;
    public static Color color;

    void OnDrawGizmosSelected()
    {        
        Gizmos.color = Color.blue;
        var addY = new Vector3(0, 1, 0);
        Gizmos.DrawSphere(transform.position+addY, 0.2f);
        for (int i = 0; i < possiblePaths.Length; i++)
        {
            Gizmos.DrawLine(transform.position + addY, possiblePaths[i].position + addY);
        }
    }

    public GameObject GetNextPath(Transform trans)
    {
        if(possiblePaths.Length == 1)
        {
            return possiblePaths[0].gameObject;
        }

        var validChoices = new List<int>();
        for(int i=0; i< possiblePaths.Length;i++)
        {
            if(possiblePaths[i] != trans)
            {
                validChoices.Add(i);
            }
        }
        var index = validChoices[Random.Range(0, validChoices.Count)];
        
        return possiblePaths[index].gameObject;
    }
}