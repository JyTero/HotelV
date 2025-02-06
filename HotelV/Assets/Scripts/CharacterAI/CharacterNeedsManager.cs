using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterNeedsManager : MonoBehaviour
{
    [HideInInspector]   //Need, value
    public Dictionary<NeedBaseSO, int> characterNeeds = new();

    private CharacterBase thisCharacter;

    [Header("Needs")]
    [SerializeField]
    private Hunger_NeedSO hungerNeedSO;
    public int HungerNeedValue;
    //public int HungerNeedValue { get => hungerNeedValue; private set => hungerNeedValue = value; }
    // [SerializeField]
    // private int hungerNeedValue;


    [SerializeField]
    private Energy_NeedSO energyNeedSO;
    public int EnergyNeedValue;
    //public int EnergyNeedValue { get => energyNeedValue; private set => energyNeedValue = value; }
    //[SerializeField]
    // private int energyNeedValue;


    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;
    private string dbString = "";
    private void Awake()
    {
        thisCharacter = GetComponent<CharacterBase>();

        //Purkka: In the future, two lists, Needs<NeedBaseSO> and needValues<int> which then get tied together into the dictionary

        characterNeeds.Add(hungerNeedSO, HungerNeedValue);
        characterNeeds.Add(energyNeedSO, EnergyNeedValue);

    }

    private int totalTics = 0;
    private int needValue = 0;
    private void Start()
    {
        StartCoroutine(NeedTimer());
    }

    private IEnumerator NeedTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            DeclineNeeds();
        }
    }

    public void DeclineNeeds()
    {
        totalTics++;
        int needValue = 0;

        if (debugEnabled)
            dbString = $"{thisCharacter.CharacterName}'s needs decline:\n";


        foreach (NeedBaseSO need in characterNeeds.Keys.ToList())
        {
            if (debugEnabled)
                dbString += $"need {need.NeedName} decline from {characterNeeds[need]} ";

            needValue = need.NeedPassiveDecline(characterNeeds[need], totalTics, this);
            characterNeeds[need] = needValue;

            if (debugEnabled)
                dbString += $"to {characterNeeds[need]}.\n";
        }
        if (debugEnabled)
            Debug.Log(dbString);

        if (totalTics >= 120)
            totalTics = 0;

    }
}
