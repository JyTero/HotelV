using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sleep_InteractionSO", menuName = "ScriptableObjects/Interactions/Sleep_InteractionSO")]
public class Sleep_InteractionSO : InteractionBaseSO
{

    public override void InteractionStart(InteractableObject interactionOwner)
    {
        base.InteractionStart(interactionOwner);
    }

    public override void BeginInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.BeginInteraction(thisCharacter, interactionOwner);

        RunInteraction(thisCharacter, interactionOwner);

    }

    public override void StartInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.StartInteraction(thisCharacter, interactionOwner);

        interactionOwner.RegisterAsActiveInteraction(thisCharacter, this, interactionOwner);

        thisCharacter.AddState(objectStatesSO.SleepState);
    }
    public override void OnInteractionTick(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.OnInteractionTick(thisCharacter, interactionOwner);

        foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
        {

            float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

            thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
        }
    }

    public override void OnInteractionEnd(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        base.OnInteractionEnd(thisCharacter, interactionOwner);

        thisCharacter.RemoveState(objectStatesSO.SleepState);
    }






}
