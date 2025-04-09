using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SocialInteractionBaseSO : InteractionBaseSO
{

    [SerializeField]
    protected int interactionRelationshipChange;
    public int InteractionRelationshipChange { get => interactionRelationshipChange; protected set => interactionRelationshipChange = value; }
    [SerializeField]
    protected List<NeedRateChangePairs> responderNeedSONeedAdjustRates = new();

    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public override void BeginInteraction(Interaction interaction)
    {
        base.BeginInteraction(interaction);
        interaction.InteractionInitiator.AddState(objectStatesSO.SocialState);

    }

    public override void RunInteraction(Interaction interaction)
    {
        base.RunInteraction(interaction);

    }

    public override void OnInteractionTick(Interaction interaction)
    {
        base.OnInteractionTick(interaction);


    }

    public override void OnInteractionEnd(Interaction interaction)
    {
        OnInteractionEnd(interaction as SocialInteraction);
        base.OnInteractionEnd(interaction);
   
    }

    protected virtual void OnInteractionEnd(SocialInteraction socInteraction)
    {
        ResponseOnInteractionEnd(socInteraction);
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

        interactionReceiver.AddState(objectStatesSO.SocialState);
        initiator.InteractionTargetReady();
    }

    public virtual void ResponseRunInteraction(CharacterBase interactionTarget, InteractableObject interactionInitator)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"Begins {thisCharacter.ObjectName} socialising with {interactionOwner.ObjectName}.");

        // interactionInitator.RegisterAsActiveInteraction(interactionTarget, this, interactionInitator);

    }

    public virtual void ResponseOnInteractionTick(CharacterBase interactionTarget, InteractableObject interactionInitator)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"{thisCharacter.ObjectName} continues socialising with {interactionOwner.ObjectName}");
    }

    public virtual void ResponseOnInteractionEnd(SocialInteraction socInteraction)
    {
        //if (interactionOwner.debugEnabled)
        //    Debug.Log($"{thisCharacter.ObjectName} stopped being social with {interactionOwner.ObjectName}");
        socInteraction.InteractionOwner.AddState(objectStatesSO.SocialState);
        ((CharacterBase)socInteraction.InteractionOwner).OnInteractionEnd();
    }

    protected void AdjustCharacterRelations(CharacterBase relationOwner, CharacterBase relationTarget, int changeValue)
    {
        relationOwner.thisCharacterRelationshipsManager.ModifyRelationship(relationTarget, changeValue);
    }

}
