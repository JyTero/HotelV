using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Vector3 currentDestination;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void SetAndSaveDestination(Vector3 destination)
    {
        currentDestination = destination;
        navAgent.SetDestination(destination);
    }

}
