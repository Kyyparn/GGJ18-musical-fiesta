using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Characters.NPC
{
    public class Monster : NonPlayerCharacter
    {
        private NavMeshAgent agent;

        private int currentTarget;
        private int previousTarget;

        private GameObject[] checkpointList;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            checkpointList = GameObject.FindGameObjectsWithTag("Waypoint");
            previousTarget = 0;
            currentTarget = GetClosestWaypoint();
            var nextPath = (checkpointList[currentTarget].GetComponent("Waypoint") as Waypoint).GetNextPath(checkpointList[previousTarget].transform);
            agent.SetDestination(nextPath.transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if (agent.remainingDistance < 0.25f)
            {
                var nextPath = (checkpointList[currentTarget].GetComponent("Waypoint") as Waypoint).GetNextPath(checkpointList[previousTarget].transform);
                agent.SetDestination(nextPath.transform.position);
                previousTarget = currentTarget;
                currentTarget = FindIndexInList(nextPath);
            }
        }

        int GetClosestWaypoint()
        {
            float distance = Mathf.Infinity;
            var closest = 0;
            for(int i=0; i<checkpointList.Length; i++)
            {
                var diff = checkpointList[i].transform.position - this.transform.position;
                var currentDistance = diff.sqrMagnitude;
                if(currentDistance < distance)
                {
                    closest = i;
                    distance = currentDistance;
                }
            }

            return closest;
        }

        int FindIndexInList(GameObject next)
        {
            for (int i = 0; i < checkpointList.Length; i++)
            {
                if (checkpointList[i] == next)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}