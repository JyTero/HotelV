using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "FeedOnCharacter_InteractionSO", menuName = "ScriptableObjects/Interactions/FeedOnCharacter_InteractionSO")]
public class FeedOnCharacter_InteractionSO : SocialInteractionBaseSO
{

    public override void InteractionStart(InteractableObject interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void BeginInteraction(Interaction interaction)
    {
        base.BeginInteraction(interaction);
        ((CharacterBase)interaction.InteractionOwner).PrepareToBeSocialTarget(this, interaction.InteractionInitiator);

    }
    public override void ContinueInteractionOnTargetReady(CharacterBase initiator, InteractableObject interactionOwner)
    {
        base.ContinueInteractionOnTargetReady(initiator, interactionOwner);
       // RouteToInteraction(interaction.InteractionInitiator, interaction.InteractionOwner);
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

    public override void OnInteractionEnd(Interaction interaction)
    {

        base.OnInteractionEnd(interaction);
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

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);

        }
    }

    public override void ResponseOnInteractionEnd(SocialInteraction socInteraction)
    {
        base.ResponseOnInteractionEnd(socInteraction);
    }

}

