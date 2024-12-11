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

    //Later for when interactions may use multiple needs to weight
    //public List<NeedBaseSO> NeedWeightsToUse { get => needWeightsToUse; private set => needWeightsToUse = Mathf.Clamp(value, -100, 100); }
    //[Tooltip("Add needs here so that they can be weighted when scoring interactions")]
    //[SerializeField]
    //private List<NeedBaseSO> needWeightsToUse;

    public NeedBaseSO NeedToUseForWeighting { get => needToUseForWeight; private set => needToUseForWeight = value; }
    [SerializeField]
    private NeedBaseSO needToUseForWeight;

    private ItemBase thisItem;

    public abstract void BeginInteraction(CharacterBase thisCharacter);
    public abstract void RunInteraction();
    public abstract void OnInteractionEnd();
}
