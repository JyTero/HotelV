using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public string ItemName { get => itemName; protected set => itemName = value; }
    public List<Transform> ItemInteractionSpots { get => itemInteractionSpots; protected set => ItemInteractionSpots = new(); }
    public List<InteractionBaseSO> ItemInteractions { get => itemInteractions; protected set => ItemInteractions = new(); }


    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected List<InteractionBaseSO> itemInteractions = new();
    [SerializeField]
    protected List<Transform> itemInteractionSpots = new();

    [HideInInspector]
    public int currentItemTick = -1;

    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;
    private string dbString = "";

    private List<ActiveInteraction> activeInteractions = new();
    protected virtual void Start()
    {
        TickManager.Instance.OnTick += CauseTick;

    }

    protected virtual void CauseTick(int newTick)
    {
        currentItemTick = newTick;
        UpdateActiveInteractions();
    }

    private void UpdateActiveInteractions()
    {
        foreach (ActiveInteraction activeInteraction in activeInteractions)
        {
            if(IsInteractionFinished(activeInteraction) == true)
            {
                //End Ineraction
            }
            else
            {
                //Update Interaction
                activeInteraction.interactionSO.OnInteractionTick(activeInteraction.interactionPefromer,activeInteraction.interactionItem);
            }
        }

        //Debug.LogWarning($"Item {ItemName} failed to match interaction to Active interaction\n" +
        //   $"Character: {thisCharacter.CharacterName}\nInteraction: {interactionSO.InteractionName}");
        //return false;
    }

    public void RegisterAsActiveInteraction(CharacterBase thisCharacter, InteractionBaseSO interactionSO, ItemBase interactionItem)
    {
        ActiveInteraction activeInteraction = new(thisCharacter, interactionSO, currentItemTick, interactionItem, itemInteractionSpots[0]);
        activeInteractions.Add(activeInteraction);
    }

    protected bool IsInteractionFinished(ActiveInteraction activeInteraction)
    {
        int ticksRemaining = InteractionTicksRemaining(activeInteraction.interactionSO.InteractionLenghtTicks,
                                                       activeInteraction.interactionStartTick);
        if (ticksRemaining <= 0)
        {
            return true;
        }
        else
            return false;


       
    }

    private int InteractionTicksRemaining(int interactionLenght, int interactionStartTicks)
    {
        return (interactionStartTicks + interactionLenght) - currentItemTick;
    }

    //TODO: Logic to automatically go though each interaction spot, return first free slot, to be used when gathering
    //interactions.


    protected virtual void OnDisable()
    {
        TickManager.Instance.OnTick -= CauseTick;
    }

    protected class ActiveInteraction
    {
        public CharacterBase interactionPefromer;
        public InteractionBaseSO interactionSO;
        public ItemBase interactionItem;
        public int interactionStartTick;
        public Transform interactionSpot;
        public int interactionCurrentTick;

        public ActiveInteraction(CharacterBase _interactionPerformer, InteractionBaseSO _interactionSO, int _interactionStartTick, ItemBase _interactionItem, Transform _interactionSpot)
        {
            this.interactionPefromer = _interactionPerformer;
            this.interactionSO = _interactionSO;
            this.interactionStartTick = _interactionStartTick;
            this.interactionItem = _interactionItem;
            this.interactionSpot = _interactionSpot;


        }
    }
}
