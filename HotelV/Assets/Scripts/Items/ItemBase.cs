using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
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

}
