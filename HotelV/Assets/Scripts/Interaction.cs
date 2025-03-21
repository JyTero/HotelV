using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public InteractionBaseSO InteractionSO;
    public string InteractionName { get; protected set; }
    public InteractableObject InteractionOwner;
    public CharacterBase InteractionInitiator;

    [HideInInspector]
    public bool InteractionEnabled = true;

    public Interaction(InteractionBaseSO interactionSO, InteractableObject interactionOwner)
    {
        InteractionSO = interactionSO;
        InteractionName = interactionSO.InteractionName;
        InteractionOwner = interactionOwner;
    }

    public virtual void BeginInteraction(CharacterBase initiator)
    {
        InteractionInitiator = initiator;
        InteractionSO.BeginInteraction(initiator, InteractionOwner);
    }

    public virtual void RunInteraction(CharacterBase initiator)
    {
        InteractionSO.RunInteraction(initiator, InteractionOwner);
    }
}

public class SocialInteraction : Interaction
{
    public SocialInteractionBaseSO SocialInteractionSO;
    public SocialInteraction(InteractionBaseSO interactionSO, InteractableObject interactionOwner)
     : base(interactionSO, interactionOwner)
    {
        SocialInteractionSO = interactionSO as SocialInteractionBaseSO;
        InteractionName = $"Respond to {interactionSO.InteractionName}";
    }

    public override void BeginInteraction(CharacterBase initiator)
    {
        InteractionInitiator = initiator;
        SocialInteractionSO.ResponseBeginInteraction((CharacterBase)InteractionOwner, initiator);
    }

    public override void RunInteraction(CharacterBase initiator)
    {
        SocialInteractionSO.ResponseRunInteraction((CharacterBase)InteractionOwner, initiator);
    }
}