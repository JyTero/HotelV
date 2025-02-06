using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HungerNeedSO", menuName = "ScriptableObjects/Needs/Hunger_NeedSO")]
public class Hunger_NeedSO : NeedBaseSO
{
    protected override void UpdateNeedValue(int newNeedValue, CharacterNeedsManager thisNeedManager)
    {
        base.UpdateNeedValue(newNeedValue, thisNeedManager);
        thisNeedManager.HungerNeedValue = newNeedValue;
    }
}
