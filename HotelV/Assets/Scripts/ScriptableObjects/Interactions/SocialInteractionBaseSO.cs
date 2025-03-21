using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SocialInteractionBaseSO : InteractionBaseSO
{
    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public override void BeginInteraction(CharacterBase initiator, InteractableObject interactionReceiver)
    {
        base.BeginInteraction(initiator, interactionReceiver);


    }

    public override void RunInteraction(CharacterBase initiator, InteractableObject interactionReceiver)
    {
        base.RunInteraction(initiator, interactionReceiver);

    }

    public override void OnInteractionTick(CharacterBase initiator, InteractableObject interactionReceiver)
    {
        base.OnInteractionTick(initiator, interactionReceiver);


    }

    public override void OnInteractionEnd(CharacterBase initiator, InteractableObject interactionReceiver)
    {
        base.OnInteractionEnd(initiator, interactionReceiver);
        ResponseOnInteractionEnd((CharacterBase)interactionReceiver, initiator);
    }
    public virtual void ContinueInteractionOnTargetReady(CharacterBase initiator, InteractableObject interactionOwner)
    {
        RouteToInteraction(initiator, interactionOwner);
    }

    public void RunReceiverInteraction(InteractableObject initiator, InteractableObject interactionReceiver)
    {
        ((CharacterBase)interactionReceiver).CurrentInteraction().RunInteraction((CharacterBase)initiator);
    }

    public virtual void ResponseBeginInteraction(CharacterBase initiator, InteractableObject interactionReceiver)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"{interactionName} started by {thisCharacter.ObjectName}");

        initiator.AddState(objectStatesSO.SocialState);
        initiator.InteractionTargetReady();
    }

    public virtual void ResponseRunInteraction(CharacterBase interactionTarget, InteractableObject interactionInitator)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"Begins {thisCharacter.ObjectName} socialising with {interactionOwner.ObjectName}.");

        interactionInitator.RegisterAsActiveInteraction(interactionTarget, this, interactionInitator);

    }

    public virtual void ResponseOnInteractionTick(CharacterBase interactionTarget, InteractableObject interactionInitator)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"{thisCharacter.ObjectName} continues socialising with {interactionOwner.ObjectName}");
    }

    public virtual void ResponseOnInteractionEnd(CharacterBase interactionTarget, InteractableObject interactionInitator)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"{thisCharacter.ObjectName} stopped being social with {interactionOwner.ObjectName}");
        interactionTarget.OnInteractionEnd();
    }

}
