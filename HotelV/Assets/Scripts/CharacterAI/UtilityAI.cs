using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UtilityAI : MonoBehaviour
{
    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;
    private string interactionSelectDebugString;


    private List<ItemBase> allItemsInWorld = new();

    private List<InteractionInScoring> foundInteractions = new();

    private InteractionInScoring currentInteraction;
    private void Start()
    {
        allItemsInWorld = FindObjectsOfType<ItemBase>().ToList<ItemBase>();
    }


    public InteractionBaseSO ChooseWhatToDo(CharacterBase thisCharacter)
    {
        if (debugEnabled)
        {
            interactionSelectDebugString = "";
            interactionSelectDebugString += $"{thisCharacter.CharacterName} what to do:\n";
        }

        GatherInteractions();
        ScoreGatheredInteractions(thisCharacter);
        SortFoundInteractionsByScore();
        InteractionBaseSO highestScoringInteractionSO = foundInteractions[0].InteractionSO;
        currentInteraction = foundInteractions[0];
        return highestScoringInteractionSO;


    }

    //TODO: add implementation get and score actions relating to the need triggering the search
    public InteractionBaseSO NeedBasedUtilityAI(CharacterBase thisCharacter)
    {
        GatherInteractions();
        ScoreGatheredInteractions(thisCharacter);
        SortFoundInteractionsByScore();
        if (HigestScoringInteractionScoreHigherThanCurrentInteraction())
        {
            InteractionBaseSO highestScoringInteractionSO = foundInteractions[0].InteractionSO;
            currentInteraction = foundInteractions[0];
            return highestScoringInteractionSO;
        }
        else
            return null;


    }

    private bool HigestScoringInteractionScoreHigherThanCurrentInteraction()
    {
        if (currentInteraction.InteractionScore < foundInteractions[0].InteractionScore)
            return true;
        else
            return false;
    }

    private void GatherInteractions()
    {
        foundInteractions.Clear();
        interactionSelectDebugString += "They found: \n";
        foreach (ItemBase item in allItemsInWorld)
        {
            foreach (InteractionBaseSO interactionSO in item.ItemInteractions)
            {
                foundInteractions.Add(new InteractionInScoring(interactionSO, item));
                if (debugEnabled)
                    interactionSelectDebugString += $"{interactionSO.InteractionName},\n";
            }
            if (debugEnabled)
            {
                interactionSelectDebugString += $"In total they found {foundInteractions.Count} interactions.\n";
                Debug.Log(interactionSelectDebugString);
                interactionSelectDebugString = "";
            }
        }
    }

    private void ScoreGatheredInteractions(CharacterBase thisCharacter)
    {
        int score;
        NeedBaseSO needSOUsedForWeighting = null;
        int needWeightedValue = 0;

        if (debugEnabled)
            interactionSelectDebugString += "The interactions are scored as follows:\n";
        foreach (InteractionInScoring interaction in foundInteractions)
        {
            score = 0;

            if (interaction.InteractionSO.NeedToUseForWeighting != null)
            {
                foreach (KeyValuePair<NeedBaseSO, int> need in thisCharacter.characterNeeds)
                {
                    if (need.Key == interaction.InteractionSO.NeedToUseForWeighting)
                    {
                        needSOUsedForWeighting = need.Key;
                        needWeightedValue = need.Value;
                    }
                }

                score += (int)(interaction.InteractionSO.InteractionBaseScore * needSOUsedForWeighting.NeedWeightCurve.Evaluate(needWeightedValue));
            }
            else
            {
                score += (int)interaction.InteractionSO.InteractionBaseScore;
            }

            interaction.InteractionScore = score;
            if (debugEnabled)
                interactionSelectDebugString += $"Interaction {interaction.InteractionSO.InteractionName} scored {interaction.InteractionScore}";
        }
    }

    private void SortFoundInteractionsByScore()
    {
        foundInteractions.Sort((a, b) => b.InteractionScore.CompareTo(a.InteractionScore));
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

