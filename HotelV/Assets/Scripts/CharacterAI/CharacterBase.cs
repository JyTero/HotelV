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

    [Header("Needs")]
    [SerializeField]
    private Hunger_NeedSO hungerNeedSO;
    public int HungerNeedValue { get => hungerNeedValue; private set => hungerNeedValue = value; }
    [SerializeField]
    private int hungerNeedValue;
    [HideInInspector]
    public Dictionary<NeedBaseSO,int> characterNeeds = new();

    private UtilityAI UtilityAI;
    private CharacterNavigation characterNavigation;

    private void Start()
    {
        UtilityAI = GetComponent<UtilityAI>();
        characterNavigation = GetComponent<CharacterNavigation>();

        hungerNeedSO.OnLowNeed += HandleLowNeedAlert;

        characterNeeds.Add(hungerNeedSO,hungerNeedValue);
    }


    //DEBUG
    private float hungerDecrease = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
           
            InteractionBaseSO interaction = UtilityAI.ChooseWhatToDo(this);
            if (interaction != null)
            {
                interaction.BeginInteraction(this);
            }
        }

        //DecreaseHunger
        hungerDecrease = hungerDecrease + (1f * Time.deltaTime);
        if(hungerDecrease > 1)
            hungerNeedSO.AdjustNeed((int)hungerDecrease, hungerNeedValue); 
    }


    private void HandleLowNeedAlert(NeedBaseSO needSO)
    {
        //Begin UtilityAI 

        InteractionBaseSO interaction = UtilityAI.NeedBasedUtilityAI(this);
        if (interaction != null) 
        {
            interaction.BeginInteraction(this);
        }

        //TODO: run Weighted Utility AI to search for need improving interactions.
    }

    public void SetDestination(Vector3 destination)
    {
        characterNavigation.SetAndSaveDestination(destination);
    }
}
