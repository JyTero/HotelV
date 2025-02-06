using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionBaseSO : ScriptableObject
{
    public string InteractionName { get => interactionName; private set => interactionName = value; }
    [SerializeField]
    private string interactionName;

    public int InteractionBaseScore { get => interactionBaseScore; private set => interactionBaseScore = Mathf.Clamp(value, -100, 100); }
    [SerializeField]
    private int interactionBaseScore;

    public int InteractionLenghtTicks { get => interactionLenghtTicks; private set => interactionBaseScore = value; }
    [SerializeField]
    private int interactionLenghtTicks;

    //Later for when interactions may use multiple needs to weight
    //public List<NeedBaseSO> NeedWeightsToUse { get => needWeightsToUse; private set => needWeightsToUse = Mathf.Clamp(value, -100, 100); }
    //[Tooltip("Add needs here so that they can be weighted when scoring interactions")]
    //[SerializeField]
    //private List<NeedBaseSO> needWeightsToUse;

    public NeedBaseSO NeedToUseForWeighting { get => needToUseForWeight; private set => needToUseForWeight = value; }
    [SerializeField]
    private NeedBaseSO needToUseForWeight;


    
    public abstract void InteractionStart(ItemBase thisItem);
    public abstract void BeginInteraction(CharacterBase thisCharacter, ItemBase thisItem);
    public abstract void RunInteraction(CharacterBase thisCharacter, ItemBase thisItem);
    public abstract void OnInteractionEnd(CharacterBase thisCharacter, ItemBase thisItem);
    public abstract void OnInteractionTick(CharacterBase thisCharacter, ItemBase thisItem);
   // public abstract IEnumerable InteractionCoro01(CharacterBase thisCharacter, ItemBase thisItem);
      
    
}
