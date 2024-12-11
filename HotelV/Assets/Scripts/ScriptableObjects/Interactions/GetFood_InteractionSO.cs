using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GetFood_InteractionSO", menuName = "ScriptableObjects/Interactions/GetFood_InteractionSO")]
public class GetFood_InteractionSO : InteractionBaseSO
{

    public override void BeginInteraction(CharacterBase thisCharacter)
    {
        Debug.Log("GetFood_Interaction started");


    }


    public override void RunInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInteractionEnd()
    {
        throw new System.NotImplementedException();
    }

}
