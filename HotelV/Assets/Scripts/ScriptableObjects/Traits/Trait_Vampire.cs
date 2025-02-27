using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trait_Vampire", menuName = "ScriptableObjects/Traits/Trait_Vampire")]
public class Trait_Vampire : TraitBaseSO
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTraitAdd(CharacterBase thisCharacter)
    {
        base.OnTraitAdd(thisCharacter);
        
    }

    public override void OnTraitRemove(CharacterBase thisCharacter)
    {
        base.OnTraitRemove(thisCharacter);
    }
}
