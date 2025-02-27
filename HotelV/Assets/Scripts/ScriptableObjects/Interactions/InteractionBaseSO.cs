using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBaseSO : ScriptableObject
{
    [SerializeField]
    protected ObjectStateHolderSO objectStatesSO;

    public string InteractionName { get => interactionName; protected set => interactionName = value; }
    [SerializeField]
    protected string interactionName;

    public int InteractionBaseScore { get => interactionBaseScore; protected set => interactionBaseScore = value; }
    [SerializeField]
    protected int interactionBaseScore;

    public int InteractionLenghtTicks { get => interactionLenghtTicks; protected set => interactionBaseScore = value; }
    [SerializeField]
    protected int interactionLenghtTicks;

    public HashSet<ObjectState_BaseSO> InvalidInteractionOwnerStates { get => invalidInteractionOwnerStates; protected set => invalidInteractionOwnerStates = value; }
    [SerializeField]
    [Tooltip("If the Object this interaction is attached to is in these states, the interaction will be disabled")]
    protected HashSet<ObjectState_BaseSO> invalidInteractionOwnerStates = new();

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

    [HideInInspector]
    public bool InteractionEnabled = true;
    
    public virtual void InteractionStart(InteractableObject interactionOwner)
    {

    }

    public virtual void BeginInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        if (interactionOwner.debugEnabled)
            Debug.Log($"{interactionName} started by {thisCharacter.ObjectName}");
        thisCharacter.ObjectStates.Remove(objectStatesSO.IdleState);
    }

    public virtual void StartInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        if (interactionOwner.debugEnabled)
            Debug.Log($"{thisCharacter.ObjectName} uses {interactionOwner.ObjectName}.");
    }

    public virtual void OnInteractionTick(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        if (interactionOwner.debugEnabled)
            Debug.Log($"{thisCharacter.ObjectName} continues using {interactionOwner.ObjectName}");
    }

    public virtual void OnInteractionEnd(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        if (interactionOwner.debugEnabled)
            Debug.Log($"{thisCharacter.ObjectName} stopped using {interactionOwner.ObjectName}");
        thisCharacter.OnInteractionEnd();

    }

    protected int NeedChangePerTick(int changePerSecond, int tickRate)
    {
        return changePerSecond / tickRate;
    }

    protected Transform GetInteractionSpot(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        Transform itemInterctionSpot = interactionOwner.GetInteractionSpot();
        if(itemInterctionSpot == null)
        {
            if (interactionOwner.debugEnabled)
                Debug.Log($"{thisCharacter.name} cannot find free interaction spots on {interactionOwner.name} ({interactionOwner.gameObject.name}). " +
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



    protected void RunInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
    {
        Transform interactionSpot = GetInteractionSpot(thisCharacter, interactionOwner);
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
