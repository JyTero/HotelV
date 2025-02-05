using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public string ItemName { get => itemName; private set => itemName = value; }
    public List<Transform> ItemInteractionSpots { get => itemInteractionSpots; private set => ItemInteractionSpots = new(); }
    public List<InteractionBaseSO> ItemInteractions { get => itemInteractions; private set => ItemInteractions = new(); }

    [SerializeField]
    private string itemName;
    [SerializeField]
    private List<InteractionBaseSO> itemInteractions = new();
    [SerializeField]
    private List<Transform> itemInteractionSpots = new();

    public abstract void RefrenceOfSelfToInteractions();

    //TODO: Logic to automatically go though each interaction spot, return first free slot.

}
