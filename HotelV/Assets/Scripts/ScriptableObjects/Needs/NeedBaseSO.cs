using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NeedBaseSO : ScriptableObject
{
    public string NeedName { get => needName; private set => needName = value; }
    [SerializeField]
    private string needName;

    public int NeedValue { get => needValue; private set => needValue = Mathf.Clamp(value, -100, 100); }
    [SerializeField]
    private int needValue;

    public AnimationCurve NeedWeightCurve { get => needWeightCurve; private set => needWeightCurve = value; }
    [SerializeField]
    private AnimationCurve needWeightCurve;

    protected virtual void AdjustNeed(int needAdjustValue)
    {
        NeedValue += needAdjustValue;
    }


}
