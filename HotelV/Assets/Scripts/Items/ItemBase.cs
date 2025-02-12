using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public string ItemName { get => itemName; protected set => itemName = value; }
    public List<InteractionBaseSO> ItemInteractions { get => itemInteractions; protected set => ItemInteractions = new(); }

    public Dictionary<Transform, bool> ItemInteractionSpots = new();

    [HideInInspector]
    public int currentItemTick = -1;

    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected List<InteractionBaseSO> itemInteractions = new();
    [SerializeField]
    protected List<Transform> itemInteractionSpots = new();


    [Header("DEBUG")]
    public bool debugEnabled;
    [HideInInspector]
    public string dbString = "";

    private List<ActiveInteraction> activeInteractions = new();
    private List<ActiveInteraction> deregisterActiveInteractions = new();

    protected virtual void Awake()
    {
        MoveInteractionSportsFromListToDictionary();
    }

    private void MoveInteractionSportsFromListToDictionary()
    {
        foreach (Transform t in itemInteractionSpots)
        {
            ItemInteractionSpots.Add(t, false);
        }
    }

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
        if (activeInteractions.Count <= 0)
            return;
        foreach (ActiveInteraction activeInteraction in activeInteractions)
        {
            if (IsInteractionFinished(activeInteraction) == true)
            {
                //End Ineraction
                activeInteraction.interactionSO.OnInteractionEnd(activeInteraction.interactionPefromer, this);
                //Mark for deregistration, deregister after iteration
                deregisterActiveInteractions.Add(activeInteraction);

            }
            else
            {
                //Update Interaction
                activeInteraction.interactionSO.OnInteractionTick(activeInteraction.interactionPefromer,
                                                                  activeInteraction.interactionItem);
            }
        }

        foreach (ActiveInteraction activeInteraction in deregisterActiveInteractions)
        {
            DeregisterAsActiveInteraction(activeInteraction.interactionPefromer, activeInteraction.interactionSO, this);
            FreeInteractionSpot(activeInteraction);
        }
        deregisterActiveInteractions.Clear();

        //Debug.LogWarning($"Item {ItemName} failed to match interaction to Active interaction\n" +
        //   $"Character: {thisCharacter.CharacterName}\nInteraction: {interactionSO.InteractionName}");
        //return false;
    }

    public Transform GetInteractionSpot()
    {
        foreach (Transform t in ItemInteractionSpots.Keys)
        {
            if (ItemInteractionSpots[t] == true)
                continue;
            else
            {
                ItemInteractionSpots[t] = true;
                return t;
            }
        }
        return null;
    }

    public bool ItemHasFreeInteractionSpots()
    {
        foreach (Transform t in ItemInteractionSpots.Keys)
        {
            if (ItemInteractionSpots[t] == true)
                continue;
            else
                return true;
        }
        return false;
    }

    public void RegisterAsActiveInteraction(CharacterBase thisCharacter, InteractionBaseSO interactionSO, ItemBase interactionItem)
    {
        ActiveInteraction activeInteraction = new(thisCharacter, interactionSO, currentItemTick, interactionItem, itemInteractionSpots[0]);
        activeInteractions.Add(activeInteraction);
    }

    public void DeregisterAsActiveInteraction(CharacterBase thisCharacter, InteractionBaseSO interactionSO, ItemBase interactionItem)
    {
        for (int i = activeInteractions.Count - 1; i >= 0; i--)
        {
            if (activeInteractions[i].interactionPefromer == thisCharacter)
            {
                if (activeInteractions[i].interactionSO == interactionSO)
                {
                    if (activeInteractions[i].interactionItem == interactionItem)
                    {
                        activeInteractions.Remove(activeInteractions[i]);
                        return;
                    }
                }

            }

        }
    }
    private void FreeInteractionSpot(ActiveInteraction activeInteraction)
    {
        foreach (Transform t in ItemInteractionSpots.Keys)
        {
            if (t == activeInteraction.interactionSpot)
            {
                ItemInteractionSpots[t] = false;
                return;
            }
            else
                continue;
        }
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
