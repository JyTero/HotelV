using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }
    public HashSet<Interaction> AllInteractions { get => allInteractions; protected set => allInteractions = value; }
    protected HashSet<Interaction> allInteractions = new();

    public HashSet<InteractableObject> AllInteractableObjects { get => allInteractableObjects; protected set => allInteractableObjects = value; }
    protected HashSet<InteractableObject> allInteractableObjects = new();

    public enum InteractionType
    {
        Sleep,
        Eat,
        Social,
    }


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
            foreach (Interaction interaction in interactable.ObjectInteractions)
            {
                allInteractions.Add(interaction);
                interaction.InteractionSO.InteractionStart(interactable);
            }
        }
    }

}
