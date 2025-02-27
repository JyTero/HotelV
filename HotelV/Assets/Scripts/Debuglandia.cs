using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Debuglandia : MonoBehaviour
{
    [SerializeField]
    private TraitBaseSO vampireTrait;
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
                selectedCharacter.thisCharacterTraitsManager.AddTrait(vampireTrait);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (selectedCharacter != null)
            {
                selectedCharacter.thisCharacterTraitsManager.RemoveTrait(vampireTrait);
            }
        }
    }


    private void OnCharacterSelected(CharacterBase _selectedCharacter)
    {
        selectedCharacter = _selectedCharacter;
    }
}
