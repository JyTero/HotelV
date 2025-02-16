using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat_InteractionSO : InteractionBaseSO
{
    public override void InteractionStart(CharacterBase interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void InitiateInteraction(CharacterBase thisCharacter, CharacterBase interactionOwner)
    {
        base.InitiateInteraction(thisCharacter, interactionOwner);

        RunInteraction(thisCharacter, interactionOwner);

    }
    public override void StartInteraction(CharacterBase thisCharacter, CharacterBase interactionOwner)
    {
        base.StartInteraction(thisCharacter, interactionOwner);

        interactionOwner.RegisterAsActiveInteraction(thisCharacter, this, interactionOwner);
    }
    public override void OnInteractionTick(CharacterBase thisCharacter, CharacterBase interactionOwner)
    {
        base.OnInteractionTick(thisCharacter, interactionOwner);

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }
    }

    public override void OnInteractionEnd(CharacterBase thisCharacter, CharacterBase interactionOwner)
    {
        base.OnInteractionEnd(thisCharacter, interactionOwner);
    }

}
