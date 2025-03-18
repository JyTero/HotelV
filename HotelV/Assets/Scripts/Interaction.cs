using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public InteractionBaseSO InteractionSO;
    public string InteractionName { get; private set; }
    public InteractableObject InteractionOwner;
    public CharacterBase InteractionInitiator;

    [HideInInspector]
    public bool InteractionEnabled = true;

    public Interaction (InteractionBaseSO interactionSO, InteractableObject interactionOwner)
    {
        InteractionSO = interactionSO;
        InteractionName = interactionSO.InteractionName;
        InteractionOwner = interactionOwner;
    }

    public void BeginInteraction(CharacterBase initiator)
    {
        InteractionInitiator = initiator;
        InteractionSO.BeginInteraction(initiator, InteractionOwner);
    }

    public void RunInteraction(CharacterBase initiator)
    {
        InteractionSO.RunInteraction(initiator, InteractionOwner);
    }
}
