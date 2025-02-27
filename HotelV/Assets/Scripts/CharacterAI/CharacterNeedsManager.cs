using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CharacterNeedsManager : MonoBehaviour
{
    [HideInInspector]
    public HashSet<NeedBase> characterNeeds = new();

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

    [HideInInspector]
    private int newNeedDefaultValue = 70;
    private int totalTicks = 0;

    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;
    private string dbString = "";

    public bool FreezeNeeds;
    private void Awake()
    {
        thisCharacter = GetComponent<CharacterBase>();

        //Purkka: In the future, two lists, Needs<NeedBaseSO> and needValues<int> which then get tied together into the dictionary

        characterNeeds.Add(new Hunger_Need(hungerNeedSO, HungerNeedValue));
        characterNeeds.Add(new Energy_Need(energyNeedSO, EnergyNeedValue));
        characterNeeds.Add(new Fun_Need(funNeedSO, FunNeedValue));

    }


    private void Start()
    {
        TickManager.Instance.OnTick += TimerTick;

    }

    private void TimerTick(int newTick)
    {
        if (!FreezeNeeds)
            DeclineNeeds();
    }

    public void DeclineNeeds()
    {
        totalTicks++;

        if (debugEnabled)
            dbString = $"{thisCharacter.ObjectName}'s needs decline:\n";
        int i = 0;
        foreach (NeedBase need in characterNeeds)
        {

            if (debugEnabled)
                dbString += $"need {need.needSO.NeedName} from {need.needValue} ";

            need.needSO.NeedPassiveDecline(need.needValue, totalTicks, this);


            if (debugEnabled)
                dbString += $"to {need.needValue}.\n";
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

    public void AddNeed(NeedBaseSO needSO)
    {
        NeedBase need = new(needSO, newNeedDefaultValue);
        if (debugEnabled)
        {
            if (characterNeeds.Contains(need))
                Debug.Log($"Tried to add trait {needSO.NeedName}, {thisCharacter.ObjectName} already has the trait!");
            else
                Debug.Log($"Trait {needSO.NeedName} added to {thisCharacter.ObjectName}!");
        }
        characterNeeds.Add(need);
    }

    public void RemoveNeed(NeedBaseSO needSO)
    {
        NeedBase need = characterNeeds.FirstOrDefault(n => n.needSO == needSO);
        if (need != null)
        {
            if (debugEnabled)
                Debug.Log($"Trait {needSO.NeedName} removed from {thisCharacter.ObjectName}!");
            characterNeeds.Remove(need);
        }
        else
        {
            if (debugEnabled)
                Debug.Log($"Tried to remove trait {needSO.NeedName}, {thisCharacter.ObjectName} doesn't have the trait!");
        }
    }

    protected virtual void OnDisable()
    {
        TickManager.Instance.OnTick -= TimerTick;
    }
}
