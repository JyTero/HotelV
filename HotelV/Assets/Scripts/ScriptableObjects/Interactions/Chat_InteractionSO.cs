using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chat_InteractionSO", menuName = "ScriptableObjects/Interactions/Chat_InteractionSO")]
public class Chat_InteractionSO : InteractionBaseSO
{
    [SerializeField]
    private InteractionBaseSO interactionRecieverInteractionSO;

    public override void InteractionStart(InteractableObject interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void BeginInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.BeginInteraction(thisCharacter, interactionOwner);


        if (interactionOwner.ObjectStates.Contains(objectStatesSO.SocialState))
            RunInteraction(thisCharacter, interactionOwner);
        else
        {
            ((CharacterBase)interactionOwner).PrepareToBeSocialTarget(interactionRecieverInteractionSO, thisCharacter);
            thisCharacter.WaitInteractionTargetToHaveSocialState(interactionOwner);
        }

    }
    public override void StartInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.StartInteraction(thisCharacter, interactionOwner);

        interactionOwner.RegisterAsActiveInteraction(thisCharacter, this, interactionOwner);

        ((CharacterBase)interactionOwner).StartSocialInteractionRecieverInteration();

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
        base.OnInteractionEnd(thisCharacter, interactionOwner);
    }

}
