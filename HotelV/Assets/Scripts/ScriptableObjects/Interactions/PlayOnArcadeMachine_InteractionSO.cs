using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayOnArcadeMachine_InteractionSO", menuName = "ScriptableObjects/Interactions/PlayOnArcadeMachine_InteractionSO")]
public class PlayOnArcadeMachine_InteractionSO : InteractionBaseSO
{
    public override void InteractionStart(ItemBase thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public override void InitiateInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        base.InitiateInteraction(thisCharacter, thisItem);

        RunInteraction(thisCharacter, thisItem);
    }


    public override void StartInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        base.StartInteraction(thisCharacter, thisItem);

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
