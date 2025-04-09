using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurnIntoVampire_InteractionSO", menuName = "ScriptableObjects/Interactions/TurnIntoVampire_InteractionSO")]
public class TurnIntoVampire_InteractionSO : InteractionBaseSO
{
    [SerializeField]
    private TraitBaseSO vampireTrait;

    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public override void BeginInteraction(Interaction interaction)
    {
        base.BeginInteraction(interaction);
        RunInteraction(interaction);

    }
    public override void RunInteraction(Interaction interaction)
    {
        base.RunInteraction(interaction);

        interaction.InteractionInitiator.thisCharacterTraitsManager.AddTrait(vampireTrait);

        OnInteractionEnd(interaction);
    }

    public override void OnInteractionTick(Interaction interaction)
    {
        base.OnInteractionTick(interaction);

    }

    public override void OnInteractionEnd(Interaction interaction)
    {
        base.OnInteractionEnd(interaction);
    }
}
