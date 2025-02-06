using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterNavigation))]
[RequireComponent(typeof(UtilityAI))]
public class CharacterBase : MonoBehaviour
{
    [Header("Character information")]
    [SerializeField]
    private string characterName;
    public string CharacterName { get => characterName; private set => characterName = value; }


    private UtilityAI UtilityAI;
    private InteractionBaseSO currentInteraction;
    private ItemBase currentInteractionItem;
    private CharacterNavigation thisCharacterNavigation;
    private CharacterNeedsManager thisCharacterNeedsManager;

    private void Start()
    {
        UtilityAI = GetComponent<UtilityAI>();
        thisCharacterNavigation = GetComponent<CharacterNavigation>();
        thisCharacterNeedsManager = GetComponent<CharacterNeedsManager>();
    }


    //DEBUG
    private float hungerDecrease = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
           
            InteractionInScoring interaction = UtilityAI.ChooseWhatToDo(this, thisCharacterNeedsManager);
            if (interaction != null)
            {
                interaction.InteractionSO.BeginInteraction(this, interaction.InteractionItem);
                currentInteraction = interaction.InteractionSO;
                currentInteractionItem = interaction.InteractionItem;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        thisCharacterNavigation.SetAndSaveDestination(destination);
    }

    public void OnAtDestination()
    {
        currentInteraction.RunInteraction(this, currentInteractionItem);
    }

    public void TriggerInteractionCoro(InteractionBaseSO interaction)
    {

    }
}
