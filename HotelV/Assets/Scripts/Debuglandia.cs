using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Debuglandia : MonoBehaviour
{
    [SerializeField]
    private TraitBaseSO talkativeTrait;
    [SerializeField]
    private TraitBaseSO vampireTrait;
    [SerializeField]
    private InteractableObject CinteractionOwner;
    [SerializeField]
    private InteractionBaseSO Cinteraction;
    private CharacterBase selectedCharacter;
    // Start is called before the first frame update
    void Start()
    {
        WorldClickHandler.Instance.CharacterSelected += OnCharacterSelected;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (selectedCharacter != null)
            {
                //Interaction turnToVampire = selectedCharacter.ObjectInteractions.
                //            FirstOrDefault(interaction => interaction.InteractionSO is TurnIntoVampire_InteractionSO);
                selectedCharacter.AddTrait(vampireTrait);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.AddTrait(talkativeTrait);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.AddInteractionToQueue(Cinteraction, CinteractionOwner);
            }
        }
    }


    private void OnCharacterSelected(CharacterBase _selectedCharacter)
    {
        selectedCharacter = _selectedCharacter;
    }
}
