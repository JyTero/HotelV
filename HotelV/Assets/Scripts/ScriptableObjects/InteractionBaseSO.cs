using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBaseSO : ScriptableObject
{
    public string InteractionName { get => interactionName; private set => interactionName = value; }
    
    [SerializeField]
    private string interactionName;

    private ItemBase thisItem;

    protected abstract void OnInteractionBegin();
    protected abstract void RunInteraction();
    protected abstract void OnInteractionEnd();
}
