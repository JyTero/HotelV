using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Chat_InteractionSO", menuName = "ScriptableObjects/Interactions/Chat_InteractionSO")]
public class Chat_InteractionSO : SocialInteractionBaseSO
{


    public override void InteractionStart(InteractableObject interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void BeginInteraction(Interaction interaction)
    {
        base.BeginInteraction(interaction);

        //Would currently allow running the interaction if target chats with someone else
        //if (interactionOwner.ObjectStates.Contains(objectStatesSO.SocialState))
        //    RouteToInteraction(thisCharacter, interactionOwner);
        //else
        //{
        ((CharacterBase)interaction.InteractionOwner).PrepareToBeSocialTarget(this, interaction.InteractionInitiator);
        // thisCharacter.WaitInteractionTargetToHaveSocialState(interactionOwner);
        //}

    }

    public override void ContinueInteractionOnTargetReady(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.ContinueInteractionOnTargetReady(thisCharacter, interactionOwner);
       // RouteToInteraction(thisCharacter, interactionOwner);
    }
    public override void RunInteraction(Interaction interaction)
    {
        base.RunInteraction(interaction);

        RunReceiverInteraction(interaction.InteractionInitiator, interaction.InteractionOwner);

        interaction.InteractionOwner.RegisterAsActiveInteraction(interaction);

        // ((CharacterBase)interactionOwner).StartSocialInteractionRecieverInteration();

    }
    public override void OnInteractionTick(Interaction interaction)
    {
        base.OnInteractionTick(interaction);

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            interaction.InteractionInitiator.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }
    }

    protected override void OnInteractionEnd(SocialInteraction socInteraction)
    {
        AdjustCharacterRelations(socInteraction.InteractionInitiator, (CharacterBase)socInteraction.InteractionOwner, socInteraction.InteractionRelationshipScoreChange);
        base.OnInteractionEnd(socInteraction);
    }


    public override void ResponseBeginInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.ResponseBeginInteraction(thisCharacter, interactionOwner);
    }

    public override void ResponseRunInteraction(CharacterBase thisCharacter, InteractableObject interactionInitator)
    {
        base.ResponseRunInteraction(thisCharacter, interactionInitator);
    }

    public override void ResponseOnInteractionTick(CharacterBase thisCharacter, InteractableObject interactionInitator)
    {
        base.ResponseOnInteractionTick(thisCharacter, interactionInitator);

        foreach (NeedRateChangePairs needPair in responderNeedSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }
    }

    public override void ResponseOnInteractionEnd(SocialInteraction socInteraction)
    {
        AdjustCharacterRelations((CharacterBase)socInteraction.InteractionOwner, socInteraction.InteractionInitiator, socInteraction.InteractionRelationshipScoreChange);
        base.ResponseOnInteractionEnd(socInteraction);
    }

}
