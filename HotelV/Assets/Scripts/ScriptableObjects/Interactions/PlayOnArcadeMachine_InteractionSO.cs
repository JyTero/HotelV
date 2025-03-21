using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayOnArcadeMachine_InteractionSO", menuName = "ScriptableObjects/Interactions/PlayOnArcadeMachine_InteractionSO")]
public class PlayOnArcadeMachine_InteractionSO : InteractionBaseSO
{
    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public override void BeginInteraction(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.BeginInteraction(thisCharacter, thisItem);

        RouteToInteraction(thisCharacter, thisItem);
    }


    public override void RunInteraction(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.RunInteraction(thisCharacter, thisItem);

        thisItem.RegisterAsActiveInteraction(thisCharacter, this, thisItem);

    }

    public override void OnInteractionTick(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.OnInteractionTick(thisCharacter, thisItem);

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);

        }

    }
    public override void OnInteractionEnd(CharacterBase thisCharacter, InteractableObject thisItem)
    {
        base.OnInteractionEnd(thisCharacter, thisItem);
    }
}
