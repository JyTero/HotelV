using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need
{
    public NeedBaseSO needSO;
    public int needValue;

    public Need(NeedBaseSO needSO, int needValue)
    {
        this.needSO = needSO;
        this.needValue = needValue;
    }

    public void AdjustNeedValue(int adjust)
    {
        needValue += adjust;
    }
}
