using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sleep_InteractionSO", menuName = "ScriptableObjects/Interactions/Sleep_InteractionSO")]
public class Sleep_InteractionSO : InteractionBaseSO
{

    public override void InteractionStart(InteractableObject interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void BeginInteraction(Interaction interaction)
    {
        base.BeginInteraction(interaction);

        RouteToInteraction(interaction.InteractionInitiator, interaction.InteractionOwner);

    }

    public override void RunInteraction(Interaction interaction)
    {
        base.RunInteraction(interaction);

        interaction.InteractionOwner.RegisterAsActiveInteraction(interaction);

        interaction.InteractionInitiator.AddState(objectStatesSO.SleepState);
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

        interaction.InteractionInitiator.RemoveState(objectStatesSO.SleepState);
    }






}
