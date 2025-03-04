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

    public bool enableDebug = false;
    private string s = "";
    public virtual void OnTraitAdd(CharacterBase thisCharacter)
    {
        s = $"{thisCharacter.ObjectName} got a new trait: {this.traitName}\n";
        AddTraitRemoveNeeds(thisCharacter);
        AddTraitAddNeeds(thisCharacter);
        thisCharacter.ChangCharacterMaterialByTrait(this);

        FindAnyObjectByType<NeedsPanel_UI>().ForceNeedsPanelRefresh();

        Debug.Log(s);
    }
    public virtual void OnTraitRemove(CharacterBase thisCharacter)
    {
        s = $"{thisCharacter.ObjectName} lost a trait: {this.traitName}\n";
        RemoveTraitRemoveNeeds(thisCharacter);
        RemoveTraitAddNeeds(thisCharacter);
        thisCharacter.RestoreDefaultMateria();

        FindAnyObjectByType<NeedsPanel_UI>().ForceNeedsPanelRefresh();

        Debug.Log(s);
    }

    private void AddTraitRemoveNeeds(CharacterBase thisCharacter)
    {
        if (needsTraitRemoves.Count != 0)
        {
            foreach (NeedBaseSO needSO in needsTraitRemoves)
            {

                if (enableDebug)
                    s += "\nNeed Removed: " + needSO.NeedName;

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
                if (enableDebug)
                    s += "\nNeed Added: " + needSO.NeedName;

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
                if (enableDebug)
                    s += "\nNeed Removed: " + needSO.NeedName;

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
                if (enableDebug)
                    s += "\nNeed Aemoved: " + needSO.NeedName;

                thisCharacter.thisCharacterNeedsManager.AddNeed(needSO);
            }
        }
    }
}
