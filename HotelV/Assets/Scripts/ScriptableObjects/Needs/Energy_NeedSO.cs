using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyNeedSO", menuName = "ScriptableObjects/Needs/Energy_NeedSO")]
public class Energy_NeedSO : NeedBaseSO
{
    protected override void UpdateNeedValue(int newNeedValue, CharacterNeedsManager thisNeedManager)
    {
        base.UpdateNeedValue(newNeedValue, thisNeedManager);
        thisNeedManager.EnergyNeedValue = newNeedValue;
    }
}
