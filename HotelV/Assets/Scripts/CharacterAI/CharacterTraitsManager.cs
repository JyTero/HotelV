using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTraitsManager : MonoBehaviour
{

    public HashSet<TraitBaseSO> CharacterTraits { get; private set; }
    private CharacterBase thisCharacter;

    public event Action OnTraitsChange;

    [Header("DEBUG")]
    public bool debugEnabled;
    [HideInInspector]
    public string dbString = "";

    private void Awake()
    {
        thisCharacter = GetComponent<CharacterBase>();
        CharacterTraits = new();
    }

    public void AddTrait(TraitBaseSO trait)
    {
        if (debugEnabled)
        {
            if (CharacterTraits.Contains(trait))
                Debug.Log($"Tried to add trait {trait.TraitName}, {thisCharacter.ObjectName} already has the trait!");
            else
                Debug.Log($"Trait {trait.TraitName} added to {thisCharacter.ObjectName}!");
        }
        CharacterTraits.Add(trait);
        trait.OnTraitAdd(thisCharacter);
        OnTraitsChange.Invoke();
    }


    public void RemoveTrait(TraitBaseSO trait)
    {
        if (debugEnabled)
        {
            if (!CharacterTraits.Contains(trait))
                Debug.Log($"Tried to remove trait {trait.TraitName}, {thisCharacter.ObjectName} doesn't have the trait!");
            else
                Debug.Log($"Trait {trait.TraitName} removed from {thisCharacter.ObjectName}!");
        }
        CharacterTraits.Remove(trait);
        trait.OnTraitRemove(thisCharacter);
        OnTraitsChange.Invoke();
    }

    public Interaction ModifyInteractionByTrait(Interaction interaction)
    {
        foreach (TraitBaseSO trait in CharacterTraits)
        {
            //Do enum, check so.type and use proper MOdifyInteraction override
            switch (interaction.InteractionSO.interactionType)
            {
                case InteractionType.Social:
                    SocialInteraction socInteraction = interaction as SocialInteraction;
                    socInteraction = trait.ModifyInteractionByTrait(socInteraction);
                    interaction = socInteraction;
                    break;
                default:
                    interaction = trait.ModifyInteractionByTrait(interaction);
                    break;
            }


        }
        return interaction;
    }
}
