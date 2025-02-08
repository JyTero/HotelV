using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sleep_InteractionSO", menuName = "ScriptableObjects/Interactions/Sleep_InteractionSO")]
public class Sleep_InteractionSO : InteractionBaseSO
{

    public override void InteractionStart(ItemBase thisItem)
    {
       base.InteractionStart(thisItem);
    }

    public override void BeginInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        base.BeginInteraction(thisCharacter, thisItem);

        thisCharacter.SetDestination(thisItem.ItemInteractionSpots[0].position);
    }

    public override void RunInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        base.RunInteraction(thisCharacter, thisItem);

        thisItem.RegisterAsActiveInteraction(thisCharacter, this, thisItem);
    }
    public override void OnInteractionTick(CharacterBase thisCharacter, ItemBase thisItem)
    {
        base.OnInteractionTick(thisCharacter, thisItem);

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }
    }

    public override void OnInteractionEnd(CharacterBase thisCharacter, ItemBase thisItem)
    {
       base.OnInteractionEnd(thisCharacter, thisItem);
    }

 




}
