using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How far character can be from destination to be concidered to be at destinaiont")]
    private float atDestinationThreshold = 2f;

    private NavMeshAgent navAgent;
    private Vector3 currentDestination;
    private CharacterBase thisCharacter;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        thisCharacter = GetComponent<CharacterBase>();
    }

    public void SetAndSaveDestination(Vector3 destination)
    {
        currentDestination = destination;
        navAgent.SetDestination(destination);
        StartCoroutine(OnDestinationTriggerCoro(destination));
       
    }


    private IEnumerator OnDestinationTriggerCoro(Vector3 destination)
    {
        yield return new WaitUntil(() => Vector3.Distance(this.gameObject.transform.position, destination)
                                                                                <= atDestinationThreshold);
        thisCharacter.OnAtDestination();
    }



}
