using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialInteractionBaseSO : InteractionBaseSO
{
    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public override void BeginInteraction(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.BeginInteraction(thisCharacter, thisItem);


    }

    public override void RunInteraction(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.RunInteraction(thisCharacter, thisItem);

    }

    public override void OnInteractionTick(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.OnInteractionTick(thisCharacter, thisItem);


    }

    public override void OnInteractionEnd(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.OnInteractionEnd(thisCharacter, thisItem);
    }
    public virtual void ContinueInteractionOnTargetReady(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        RouteToInteraction(thisCharacter, interactionOwner);
    }

    public void RunReceiverInteraction(InteractableObject interactionInitiator, InteractableObject interactionReceiver)
    {
        ((CharacterBase)interactionReceiver).CurrentInteraction().InteractionSO.RunInteraction((CharacterBase)interactionReceiver, interactionInitiator);
    }
}
