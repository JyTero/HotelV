using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GetFood_InteractionSO", menuName = "ScriptableObjects/Interactions/GetFood_InteractionSO")]
public class GetFood_InteractionSO : InteractionBaseSO
{
    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
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

}
