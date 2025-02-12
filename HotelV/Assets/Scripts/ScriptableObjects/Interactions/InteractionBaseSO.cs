using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBaseSO : ScriptableObject
{
    public string InteractionName { get => interactionName; protected set => interactionName = value; }
    [SerializeField]
    protected string interactionName;

    public int InteractionBaseScore { get => interactionBaseScore; protected set => interactionBaseScore = Mathf.Clamp(value, -100, 100); }
    [SerializeField]
    protected int interactionBaseScore;

    public int InteractionLenghtTicks { get => interactionLenghtTicks; protected set => interactionBaseScore = value; }
    [SerializeField]
    protected int interactionLenghtTicks;

    [SerializeField]
    protected List<NeedRateChangePairs> needSONeedAdjustRates = new();

    //Later for when interactions may use multiple needs to weight
    //public List<NeedBaseSO> NeedWeightsToUse { get => needWeightsToUse; private set => needWeightsToUse = Mathf.Clamp(value, -100, 100); }
    //[Tooltip("Add needs here so that they can be weighted when scoring interactions")]
    //[SerializeField]
    //private List<NeedBaseSO> needWeightsToUse;

    public NeedBaseSO NeedToUseForWeighting { get => needToUseForWeight; protected set => needToUseForWeight = value; }
    [SerializeField]
    private NeedBaseSO needToUseForWeight;


    
    public virtual void InteractionStart(ItemBase thisItem)
    {

    }
    public virtual void InitiateInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        if (thisItem.debugEnabled)
            Debug.Log("GetFood_Interaction started");
    }
    public virtual void StartInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        if (thisItem.debugEnabled)
            Debug.Log($"{thisCharacter.CharacterName} uses {thisItem.ItemName}.");
    }
    public virtual void OnInteractionTick(CharacterBase thisCharacter, ItemBase thisItem)
    {
        if (thisItem.debugEnabled)
            Debug.Log($"{thisCharacter.CharacterName} continues using {thisItem.ItemName}");
    }
    public virtual void OnInteractionEnd(CharacterBase thisCharacter, ItemBase thisItem)
    {
        if (thisItem.debugEnabled)
            Debug.Log($"{thisCharacter.CharacterName} stopped using {thisItem.ItemName}");
        thisCharacter.OnInteractionEnd();

    }

    protected int NeedChangePerTick(int changePerSecond, int tickRate)
    {
        return changePerSecond / tickRate;
    }

    protected Transform GetInteractionSpot(CharacterBase thisCharacter, ItemBase thisItem)
    {
        Transform itemInterctionSpot = thisItem.GetInteractionSpot();
        if(itemInterctionSpot == null)
        {
            if (thisItem.debugEnabled)
                Debug.Log($"{thisCharacter.name} cannot find free interaction spots on {thisItem.name} ({thisItem.gameObject.name}). " +
                    $"Selecting new interaction");
            //Select 2nd highest scoring interaction
            return null;
        }
            else
                return itemInterctionSpot;
    }

    protected bool CanRunInteraction(Transform interactionSpot)
    {
        if (interactionSpot == null)
            return false;
        else
            return true;
    }

    protected void RunInteraction(CharacterBase thisCharacter, ItemBase thisItem)
    {
        Transform interactionSpot = GetInteractionSpot(thisCharacter, thisItem);
        if (CanRunInteraction(interactionSpot))
        {
            thisCharacter.SetDestination(interactionSpot.position);
        }
        else { }
    }

    [System.Serializable]
    public class NeedRateChangePairs
    {
        public NeedBaseSO needSO;
        public int needChangePerSecond;

        public NeedRateChangePairs(NeedBaseSO need, int needChangePerSecond)
        {
            this.needSO = need;
            this.needChangePerSecond = needChangePerSecond;
        }   
    }
}
