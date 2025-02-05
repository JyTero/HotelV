using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser_Item : ItemBase
{
    private void Start()
    {
        RefrenceOfSelfToInteractions();
    }

    public override void RefrenceOfSelfToInteractions()
    {
        foreach (InteractionBaseSO interaction in ItemInteractions)
        {
            interaction.thisItem = this;
            interaction.InteractionStart(this);
        }
    }
}
