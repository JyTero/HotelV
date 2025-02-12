using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CharacterNeedsManager : MonoBehaviour
{
    [HideInInspector]
    public List<NeedBase> characterNeeds = new List<NeedBase>();

    private CharacterBase thisCharacter;

    [Header("Needs")]
    [SerializeField]
    private Hunger_NeedSO hungerNeedSO;
    public int HungerNeedValue;

    [SerializeField]
    private Energy_NeedSO energyNeedSO;
    public int EnergyNeedValue;

    [SerializeField]
    private Fun_NeedSO funNeedSO;
    public int FunNeedValue;

    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;
    private string dbString = "";
    private void Awake()
    {
        thisCharacter = GetComponent<CharacterBase>();

        //Purkka: In the future, two lists, Needs<NeedBaseSO> and needValues<int> which then get tied together into the dictionary

        characterNeeds.Add(new Hunger_Need(hungerNeedSO, HungerNeedValue));
        characterNeeds.Add(new Energy_Need(energyNeedSO, EnergyNeedValue));
        characterNeeds.Add(new Fun_Need(funNeedSO, FunNeedValue));

    }

    private int totalTicks = 0;
    private int needValue = 0;
    private void Start()
    {
        TickManager.Instance.OnTick += TimerTick;

    }

    private void TimerTick(int newTick)
    {
        DeclineNeeds();
    }

    public void DeclineNeeds()
    {
        totalTicks++;

        if (debugEnabled)
            dbString = $"{thisCharacter.CharacterName}'s needs decline:\n";
        int i = 0;
        foreach (NeedBase need in characterNeeds) 
        {

            if (debugEnabled)
                dbString += $"need {need.needSO.NeedName} from {characterNeeds[i].needValue} ";

            need.needSO.NeedPassiveDecline(need.needValue, totalTicks, this);


            if (debugEnabled)
                dbString += $"to {characterNeeds[i].needValue}.\n";
            i++;
        }
        if (debugEnabled)
            Debug.Log(dbString);

        if (totalTicks >= 120)
            totalTicks = 0;

    }

    public void AdjustNeed(NeedBaseSO adjustNeed, int adjustValue)
    {
        foreach (NeedBase need in characterNeeds)
        {
            if (need.needSO == adjustNeed)
            {
                need.AdjustNeedValue(adjustValue, need, this);
                return;
            }
            else
                continue;
        }
    }

    protected virtual void OnDisable()
    {
        TickManager.Instance.OnTick -= TimerTick;
    }
}
