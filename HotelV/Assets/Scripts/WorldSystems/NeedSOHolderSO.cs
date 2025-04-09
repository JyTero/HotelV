using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterNeedSOHolderSO", menuName = "ScriptableObjects/Tools/CharacterNeedSOHolderSO")]
public class NeedSOHolderSO : ScriptableObject
{
    public HashSet<NeedBaseSO> NeedsHash = new();

    public Hunger_NeedSO HungerNeedSO;
    public Energy_NeedSO EnergyNeedSO;
    public Fun_NeedSO FunNeedSO;
    public Blood_NeedSO BloodNeedSO;
    public Social_NeedSO SocialNeedSO;

    public void CharacterNeedSOHolderStart()
    {
        NeedsHash.Add(HungerNeedSO);
        NeedsHash.Add(EnergyNeedSO);
        NeedsHash.Add(FunNeedSO);
        NeedsHash.Add(BloodNeedSO);
        NeedsHash.Add(SocialNeedSO);
    }

}
