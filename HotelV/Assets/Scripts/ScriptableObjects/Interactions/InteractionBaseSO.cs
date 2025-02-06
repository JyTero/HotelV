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


    
    public abstract void InteractionStart(ItemBase thisItem);
    public abstract void BeginInteraction(CharacterBase thisCharacter, ItemBase thisItem);
    public abstract void RunInteraction(CharacterBase thisCharacter, ItemBase thisItem);
    public abstract void OnInteractionEnd(CharacterBase thisCharacter, ItemBase thisItem);
    public abstract void OnInteractionTick(CharacterBase thisCharacter, ItemBase thisItem);
    // public abstract IEnumerable InteractionCoro01(CharacterBase thisCharacter, ItemBase thisItem);


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
