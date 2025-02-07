using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedBase
{
    public NeedBaseSO needSO;
    public int needValue;

    public NeedBase(NeedBaseSO needSO, int needValue)
    {
        this.needSO = needSO;
        this.needValue = needValue;
    }

    

    public virtual void AdjustNeedValue(int adjust, NeedBase adjustNeed, CharacterNeedsManager thisNeedsManager)
    {
        needValue += adjust;
    }
}

public class Hunger_Need : NeedBase
{
    public Hunger_Need(Hunger_NeedSO needSO, int needValue)
        : base(needSO, needValue)
    {
        this.needSO = needSO;
        this.needValue = needValue;
    }

    public override void AdjustNeedValue(int adjust, NeedBase adjustNeed, CharacterNeedsManager thisNeedsManager)
    {
        base.AdjustNeedValue(adjust, adjustNeed, thisNeedsManager);
        thisNeedsManager.HungerNeedValue = needValue;

    }
}
public class Energy_Need : NeedBase
{
    public Energy_Need(Energy_NeedSO needSO, int needValue)
        : base(needSO, needValue)
    {
        this.needSO = needSO;
        this.needValue = needValue;
    }

    public override void AdjustNeedValue(int adjust, NeedBase adjustNeed, CharacterNeedsManager thisNeedsManager)
    {
        base.AdjustNeedValue(adjust, adjustNeed, thisNeedsManager);
        thisNeedsManager.EnergyNeedValue = needValue;

    }
}
