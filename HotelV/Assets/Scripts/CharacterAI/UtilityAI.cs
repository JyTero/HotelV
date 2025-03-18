using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class UtilityAI : MonoBehaviour
{

    private string interactionSelectDebugString;


    //TODO: Move to item manager, now each AI has its copy of the same list

    private List<InteractionInScoring> foundInteractions = new();

    private InteractionInScoring currentInteraction;

    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;

    private void Start()
    {

    }


    public Interaction ChooseWhatToDo(CharacterBase thisCharacter, CharacterNeedsManager thisNeedsManager)
    {
        if (debugEnabled)
        {
            interactionSelectDebugString = "";
            interactionSelectDebugString += $"{thisCharacter.ObjectName} ponders what to do:\n";
        }

        GatherInteractions(thisCharacter);
        ScoreGatheredInteractions(thisCharacter, thisNeedsManager);
        SortFoundInteractionsByScore();
        Interaction highestScoringInteraction = foundInteractions[0].Interaction;
        currentInteraction = foundInteractions[0];

        if (debugEnabled)
        {
            interactionSelectDebugString += $"They chose to {currentInteraction.Interaction.InteractionName}\n";
            Debug.Log(interactionSelectDebugString);
        }
        return highestScoringInteraction;


    }

    //TODO: add implementation get and score actions relating to the need triggering the search
    public Interaction NeedBasedUtilityAI(CharacterBase thisCharacter, CharacterNeedsManager thisNeedsManager)
    {
        GatherInteractions(thisCharacter);
        ScoreGatheredInteractions(thisCharacter, thisNeedsManager);
        SortFoundInteractionsByScore();
        if (HigestScoringInteractionScoreHigherThanCurrentInteraction())
        {
            Interaction highestScoringInteraction = foundInteractions[0].Interaction;
            currentInteraction = foundInteractions[0];
            return highestScoringInteraction;
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

    private void GatherInteractions(CharacterBase thisCharacter)
    {
        foundInteractions.Clear();
        if (debugEnabled)
        {
            interactionSelectDebugString += "They found:\n";
        }
        foreach (InteractableObject interactable in InteractionManager.Instance.AllInteractableObjects)
        {
            if (interactable == thisCharacter)
                continue;

            if (interactable.ItemHasFreeInteractionSpots())
            {
                foreach (Interaction interaction in interactable.ObjectInteractions)
                {
                    if (debugEnabled)
                        interactionSelectDebugString += $"{interaction.InteractionName}, ";

                    if (CharacterHasForbiddenTraits(interaction.InteractionSO, thisCharacter))
                    {
                        if (debugEnabled)
                            interactionSelectDebugString += "not valid\n";
                        continue;
                    }
                    else
                    {
                        if (interaction.InteractionEnabled)
                        {
                            if (debugEnabled)
                                interactionSelectDebugString += "valid\n";
                            foundInteractions.Add(new InteractionInScoring(interaction));
                        }
                        else
                        {
                            if (debugEnabled)
                                interactionSelectDebugString += "not valid\n";
                            continue;
                        }
                    }

                }
            }
            else
                continue;

        }
        if (debugEnabled)
            interactionSelectDebugString += $"In total they found {foundInteractions.Count} interactions.\n";
    }

    private bool CharacterHasForbiddenTraits(InteractionBaseSO interactionSO, CharacterBase thisCharacter)
    {
        if (interactionSO.ForbiddenTraits.Count > 0)
            return false;
         else if (thisCharacter.thisCharacterTraitsManager.CharacterTraits.Overlaps(interactionSO.ForbiddenTraits))
            return true;
        else
            return false;
    }
    private void ScoreGatheredInteractions(CharacterBase thisCharacter, CharacterNeedsManager thisNeedManager)
    {
        int score;
        NeedBaseSO needSOUsedForWeighting = null;
        float weightedNeedValue = 0;

        string interactionScoreBreakdownDebug = "";
        if (debugEnabled)
            interactionSelectDebugString += "The interactions are scored as follows:\n";

        foreach (InteractionInScoring interactionInScoring in foundInteractions)
        {
            score = 0;
            interactionScoreBreakdownDebug = "";
            if (interactionInScoring.Interaction.InteractionSO.NeedToUseForWeighting != null)
            {
                foreach (NeedBase need in thisNeedManager.characterNeeds)
                {
                    needSOUsedForWeighting = null;
                    weightedNeedValue = 0;

                    if (need.needSO == interactionInScoring.Interaction.InteractionSO.NeedToUseForWeighting)
                    {
                        needSOUsedForWeighting = need.needSO;
                        weightedNeedValue = (float)need.needValue / 100;
                        break;
                    }
                }

                //If character does not have the need interaction uses for weighting
                if (needSOUsedForWeighting == null)
                {
                    score = (int)(interactionInScoring.Interaction.InteractionSO.InteractionBaseScore) / 2;
                    interactionScoreBreakdownDebug = $"Character did not have weighted need ({interactionInScoring.Interaction.InteractionSO.NeedToUseForWeighting.NeedName})";
                }
                else
                {
                    score += (int)(interactionInScoring.Interaction.InteractionSO.InteractionBaseScore * needSOUsedForWeighting.NeedWeightCurve.Evaluate(weightedNeedValue));
                    if (debugEnabled)
                        interactionScoreBreakdownDebug = $"interactionBase: {interactionInScoring.Interaction.InteractionSO.InteractionBaseScore} * " +
                                                         $"needValue ({needSOUsedForWeighting.NeedName}): {needSOUsedForWeighting.NeedWeightCurve.Evaluate(weightedNeedValue)}";
                }

            }
            else
            {
                score += (int)interactionInScoring.Interaction.InteractionSO.InteractionBaseScore;
            }

            interactionInScoring.InteractionScore = score;
            if (debugEnabled)
                interactionSelectDebugString += $"Interaction {interactionInScoring.Interaction.InteractionName} score: {interactionInScoring.InteractionScore} ({interactionScoreBreakdownDebug})\n";
            //interactionSelectDebugString += $"Interaction {interaction.InteractionSO.InteractionName} scored {interaction.InteractionScore}\n";
        }
    }

    private void SortFoundInteractionsByScore()
    {
        foundInteractions.Sort((a, b) => b.InteractionScore.CompareTo(a.InteractionScore));
    }
}


public class InteractionInScoring
{
    public Interaction Interaction;
    public int InteractionScore = 0;

    public InteractionInScoring(Interaction interaction)
    {
        Interaction = interaction;

    }
}


