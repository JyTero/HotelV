using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sleep_InteractionSO", menuName = "ScriptableObjects/Interactions/Sleep_InteractionSO")]
public class Sleep_InteractionSO : InteractionBaseSO
{

    public override void InteractionStart(ItemBase interactionOwner)
    {
       base.InteractionStart(interactionOwner);
    }

    public override void InitiateInteraction(CharacterBase thisCharacter, ItemBase interactionOwner)
    {
        base.InitiateInteraction(thisCharacter, interactionOwner);

        RunInteraction(thisCharacter, interactionOwner);

    }

    public override void StartInteraction(CharacterBase thisCharacter, ItemBase interactionOwner)
    {
        base.StartInteraction(thisCharacter, interactionOwner);

        interactionOwner.RegisterAsActiveInteraction(thisCharacter, this, interactionOwner);
    }
    public override void OnInteractionTick(CharacterBase thisCharacter, ItemBase interactionOwner)
    {
        base.OnInteractionTick(thisCharacter, interactionOwner);

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }
    }

    public override void OnInteractionEnd(CharacterBase thisCharacter, ItemBase interactionOwner)
    {
       base.OnInteractionEnd(thisCharacter, interactionOwner);
    }

 




}
