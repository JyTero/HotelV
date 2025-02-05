using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GetFood_InteractionSO", menuName = "ScriptableObjects/Interactions/GetFood_InteractionSO")]
public class GetFood_InteractionSO : InteractionBaseSO
{
    FoodDispenser_Item thisItemTrueType;

    public override void InteractionStart(ItemBase ownerItem)
    {
        thisItem = ownerItem;
        thisItemTrueType = thisItem as FoodDispenser_Item;

    }

    public override void BeginInteraction(CharacterBase thisCharacter)
    {
        Debug.Log("GetFood_Interaction started");

        thisCharacter.SetDestination(thisItem.ItemInteractionSpots[0].position);
    }


    public override void RunInteraction(CharacterBase thisCharacter)
    {
        Debug.Log($"{thisCharacter.CharacterName} uses {thisItem.ItemName}.");
    }

    public override void OnInteractionEnd(CharacterBase thisCharacter)
    {
        throw new System.NotImplementedException();
    }

}
