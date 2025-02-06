using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GetFood_InteractionSO", menuName = "ScriptableObjects/Interactions/GetFood_InteractionSO")]
public class GetFood_InteractionSO : InteractionBaseSO
{
    public override void InteractionStart(ItemBase thisItem)
    {


    }

    public override void BeginInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        Debug.Log("GetFood_Interaction started");

        thisCharacter.SetDestination(thisItem.ItemInteractionSpots[0].position);
    }


    public override void RunInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        Debug.Log($"{thisCharacter.CharacterName} uses {thisItem.ItemName}.");

        thisItem.RegisterAsActiveInteraction(thisCharacter, this, thisItem);

    }

    public override void OnInteractionTick(CharacterBase thisCharacter, ItemBase thisItem)
    {
       foreach(NeedRateChangePairs needPair in needSONeedAdjustRates)
        {
            float needChangePerTick = needPair.needChangePerSecond/TickManager.Instance.TickCounter;

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }

    }
    public override void OnInteractionEnd(CharacterBase thisCharacter, ItemBase thisItem)
    {
        throw new System.NotImplementedException();
    }

}
