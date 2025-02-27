using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeChattedWith_InteractionSO", menuName = "ScriptableObjects/Interactions/BeChattedWith_InteractionSO")]

public class BeChattedWith_InteractionSO : InteractionBaseSO
{
    public override void InteractionStart(InteractableObject interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void BeginInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.BeginInteraction(thisCharacter, interactionOwner);

        thisCharacter.AddState(objectStatesSO.SocialState);
    }
    public override void StartInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.StartInteraction(thisCharacter, interactionOwner);

        interactionOwner.RegisterAsActiveInteraction(thisCharacter, this, interactionOwner);

    }
    public override void OnInteractionTick(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.OnInteractionTick(thisCharacter, interactionOwner);

        //Commented out as social need is not implemented 
        //foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        //{

        //    float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

        //    thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        //}
    }

    public override void OnInteractionEnd(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        thisCharacter.RemoveState(objectStatesSO.SocialState);

        base.OnInteractionEnd(thisCharacter, interactionOwner);

    }
}
