using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarnTrait_InteractionSO", menuName = "ScriptableObjects/Interactions/EarnTrait_InteractionSO")]
public class EarnTrait_InteractionSO : InteractionBaseSO
{

    public override void InteractionStart(InteractableObject thisItem)
    {
        base.InteractionStart(thisItem);
    }

    public void BeginInteraction(Interaction interaction, TraitBaseSO trait)
    {
        base.BeginInteraction(interaction);
        RunInteraction(interaction, trait);

    }
    public void RunInteraction(Interaction interaction, TraitBaseSO trait)
    {
        base.RunInteraction(interaction);

        interaction.InteractionInitiator.thisCharacterTraitsManager.AddTrait(trait);

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
