using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }
    public HashSet<InteractionBaseSO> AllInteractions { get => allInteractions; protected set => allInteractions = value; }
    protected HashSet<InteractionBaseSO> allInteractions = new();

    public HashSet<InteractableObject> AllInteractableObjects { get => allInteractableObjects; protected set => allInteractableObjects = value; }
    protected HashSet<InteractableObject> allInteractableObjects = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        allInteractableObjects = FindObjectsOfType<InteractableObject>().ToHashSet<InteractableObject>();

        InitializeAllInteractionsToGlobalHashSet();
        Debug.Log("Interactions initialized");

    }

    private void InitializeAllInteractionsToGlobalHashSet()
    {
        foreach (InteractableObject interactable in allInteractableObjects)
        {
            foreach (InteractionBaseSO interaction in interactable.ObjectInteractions)
            {
                allInteractions.Add(interaction);
                interaction.InteractionStart(interactable);
            }
        }
    }

}
