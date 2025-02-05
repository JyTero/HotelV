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

    [SerializeField]
    public int lowNeedThreshold;


    public int NeedPassiveDecline(int needValue, int ellapsedTicksSinceLastDecline)
    {
        if(ellapsedTicksSinceLastDecline %  needDeclineRate == 0)
        {
            return AdjustNeedValue(needValue, -1); //1 need decline per x frames
        }
        return needValue;
    }

    public int AdjustNeedValue(int needValue, int needValueChange)
    {
        needValue += needValueChange;
        return needValue;
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
