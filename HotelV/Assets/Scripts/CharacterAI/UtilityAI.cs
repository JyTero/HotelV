using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UtilityAI : MonoBehaviour
{
    private List<ItemBase> allItemsInWorld = new();

    private List<InteractionInScoring> foundInteractions = new();

    private void Start()
    {
        allItemsInWorld = FindObjectsOfType<ItemBase>().ToList<ItemBase>();
    }


    //public InteractionBaseSO ChooseWhatToDo()
    //{

    //}

    private void GatherInteractions()
    {
        foreach (ItemBase item in allItemsInWorld)
        {
            foreach (InteractionBaseSO interactionSO in item.ItemInteractions)
            {
                foundInteractions.Add(new InteractionInScoring(interactionSO, item));
            }
        }
    }

    private void ScoreGatheredInteractions(CharacterBase thisCharacter)
    {
        int score;
        NeedBaseSO needWeighted = null;
        foreach (InteractionInScoring interaction in foundInteractions)
        {
            score = 0;

            if (interaction.InteractionSO.NeedToUseForWeighting != null)
            {
                foreach (NeedBaseSO needSO in thisCharacter.CharacterNeeds)
                {
                    if (needSO == interaction.InteractionSO.NeedToUseForWeighting)
                    {
                        needWeighted = needSO;
                    }
                }

                score += (int)(interaction.InteractionSO.InteractionBaseScore * needWeighted.NeedWeightCurve.Evaluate(needWeighted.NeedValue)); ;
            }
            else
            {
                score += (int)interaction.InteractionSO.InteractionBaseScore;
            }

            interaction.InteractionScore = score;
        }
    }

    private class InteractionInScoring
    {
        public InteractionBaseSO InteractionSO;
        public ItemBase InteractionItem;
        public int InteractionScore = 0;

        public InteractionInScoring(InteractionBaseSO interactionBaseSO, ItemBase interactionItem)
        {
            InteractionSO = interactionBaseSO;
            InteractionItem = interactionItem;
        }
    }

}

