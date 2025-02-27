using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterTraitsManager : MonoBehaviour
{

    public HashSet<TraitBaseSO> CharacterTraits => characterTraits;
    [SerializeField]
    protected HashSet<TraitBaseSO> characterTraits = new();


    private CharacterBase thisCharacter;

    [Header("DEBUG")]
    public bool debugEnabled;
    [HideInInspector]
    public string dbString = "";

    private void Awake()
    {
        thisCharacter = GetComponent<CharacterBase>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }


 
}
