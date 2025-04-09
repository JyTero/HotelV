using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vampire_TraitSO", menuName = "ScriptableObjects/Traits/Vampire_TraitSO")]
public class Vampire_TraitSO : TraitBaseSO
{

    [SerializeField]
    protected Material vampireCharacterMaterial;
    public Material VampireCharacterMaterial { get => vampireCharacterMaterial; protected set => vampireCharacterMaterial = value; }

    public override void OnTraitAdd(CharacterBase thisCharacter)
    {
        base.OnTraitAdd(thisCharacter);
        thisCharacter.ChangCharacterMaterial(vampireCharacterMaterial);
    }

    public override void OnTraitRemove(CharacterBase thisCharacter)
    {
        base.OnTraitRemove(thisCharacter);
    }

    public override Interaction ModifyInteractionByTrait(Interaction interaction)
    {
       return interaction;
    }
}
