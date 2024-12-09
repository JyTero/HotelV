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
    public int lowNeedThreshold;

    

    public virtual int AdjustNeed(int needAdjustValue, int needValue)
    {
        needValue += needAdjustValue;
        Mathf.Clamp(needValue, -100, 100);

        if (needValue < lowNeedThreshold)
        {
            OnLowNeed?.Invoke(this);
        }

        return needValue;
    }


}
