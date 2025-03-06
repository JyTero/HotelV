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

        //Would currently allow running the interaction if target chats with someone else
        if (interactionOwner.ObjectStates.Contains(objectStatesSO.SocialState))
            RouteToInteraction(thisCharacter, interactionOwner);
        else
        {
            ((CharacterBase)interactionOwner).PrepareToBeSocialTarget(interactionRecieverInteractionSO, thisCharacter);
            thisCharacter.WaitInteractionTargetToHaveSocialState(interactionOwner);
        }

    }
    public override void RunInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.RunInteraction(thisCharacter, interactionOwner);

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
