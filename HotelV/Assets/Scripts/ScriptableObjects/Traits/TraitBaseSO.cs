using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TraitBaseSO : ScriptableObject
{
    public string TraitName { get => TraitName; protected set => traitName = value; }
    [SerializeField]
    protected string traitName;

    [SerializeField]
    protected List<NeedBaseSO> needsTraitAdds = new();
    [SerializeField]
    protected List<NeedBaseSO> needsTraitRemoves = new();

    [SerializeField]
    protected Material traitCharacterAppearanceMaterial;
    public Material TraitCharacterAppearanceMaterial { get => traitCharacterAppearanceMaterial; protected set => traitCharacterAppearanceMaterial = value; }



    public virtual void OnTraitAdd(CharacterBase thisCharacter)
    {

        AddTraitRemoveNeeds(thisCharacter);
        AddTraitAddNeeds(thisCharacter);
        thisCharacter.ChangCharacterMaterialByTrait(this);
    }
    public virtual void OnTraitRemove(CharacterBase thisCharacter)
    {
        RemoveTraitRemoveNeeds(thisCharacter);
        RemoveTraitAddNeeds(thisCharacter);
        thisCharacter.RestoreDefaultMateria();
    }

    private void AddTraitRemoveNeeds(CharacterBase thisCharacter)
    {
        if (needsTraitRemoves.Count != 0)
        {
            foreach (NeedBaseSO needSO in needsTraitRemoves)
            {
                thisCharacter.thisCharacterNeedsManager.RemoveNeed(needSO);

            }
        }
    }
    private void AddTraitAddNeeds(CharacterBase thisCharacter)
    {
        if (needsTraitAdds.Count != 0)
        {
            foreach (NeedBaseSO needSO in needsTraitAdds)
            {
                thisCharacter.thisCharacterNeedsManager.AddNeed(needSO);
            }
        }
    }

    private void RemoveTraitRemoveNeeds(CharacterBase thisCharacter)
    {
        if (needsTraitAdds.Count != 0)
        {
            foreach (NeedBaseSO needSO in needsTraitRemoves)
            {
                thisCharacter.thisCharacterNeedsManager.RemoveNeed(needSO);

            }
        }
    }

    private void RemoveTraitAddNeeds(CharacterBase thisCharacter)
    {
        if (needsTraitRemoves.Count != 0)
        {
            foreach (NeedBaseSO needSO in needsTraitAdds)
            {
                thisCharacter.thisCharacterNeedsManager.AddNeed(needSO);
            }
        }
    }
}
