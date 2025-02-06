using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class NeedBaseSO : ScriptableObject
{
    public event Action<NeedBaseSO> OnLowNeed;

    public string NeedName { get => needName; private set => needName = value; }
    [SerializeField]
    private string needName;

    public AnimationCurve NeedWeightCurve { get => needWeightCurve; private set => needWeightCurve = value; }
    [SerializeField]
    private AnimationCurve needWeightCurve;

    [SerializeField]
    [Tooltip("Decline 1 need value every X ticks")]
    private float needDeclineRate;

    private int needDeclineAmmount = -1;

    [SerializeField]
    public int lowNeedThreshold;

    //protected virtual void UpdateNeedValue(int newNeedValue, CharacterNeedsManager thisNeedManager) 
    //{
    //    Mathf.Clamp(newNeedValue, 0, 100);
    //    thisNeedManager.characterNeeds[this] = newNeedValue;

    //}
    

    public void NeedPassiveDecline(int needValue, int ellapsedTicks, CharacterNeedsManager thisNeedManager)
    {
        if(ellapsedTicks %  needDeclineRate == 0)
        {
           thisNeedManager.AdjustNeed(this, needDeclineAmmount) ; //1 need decline per x frames
        }

    }




    //public virtual int AdjustNeedValue(int needAdjustValue, int needValue)
    //{
    //    needValue += needAdjustValue;
    //    Mathf.Clamp(needValue, 0, 100);

    //    if (needValue < lowNeedThreshold)
    //    {
    //        OnLowNeed?.Invoke(this);
    //    }

    //    return needValue;
    //}

    //Adjust
    //return should be different methods, not all need retrun ,they can ask for it if need be

}
