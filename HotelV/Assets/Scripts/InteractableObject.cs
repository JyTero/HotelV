using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string ObjectName { get => objectName; protected set => objectName = value; }
    [SerializeField]
    protected string objectName;

    public List<InteractionBaseSO> ObjectInteractions { get => objectInteractions; protected set => ObjectInteractions = new(); }

    public Dictionary<Transform, bool> ObjectInteractionSpots = new();

    [SerializeField]
    protected List<InteractionBaseSO> objectInteractions = new();
    [SerializeField]
    protected List<Transform> objectInteractionSpots = new();

    [HideInInspector]
    public int currentObjectTick = -1;

    private List<ActiveInteraction> activeInteractions = new();
    private List<ActiveInteraction> deregisterActiveInteractions = new();

    [Header("States")]
    [SerializeField]
    protected ObjectStateHolderSO objectStatesSO;
    public HashSet<ObjectState_BaseSO> ObjectStates { get => currentStates; protected set => currentStates = value; }
    protected HashSet<ObjectState_BaseSO> currentStates = new();

    [Header("DEBUG")]
    public bool debugEnabled;
    [HideInInspector]
    public string dbString = "";

    protected virtual void Awake()
    {
        MoveInteractionSportsFromListToDictionary();
    }

    private void MoveInteractionSportsFromListToDictionary()
    {
        foreach (Transform t in objectInteractionSpots)
        {
            ObjectInteractionSpots.Add(t, false);
        }
    }

    protected virtual void Start()
    {
        TickManager.Instance.OnTick += CauseTick;

    }

    protected virtual void CauseTick(int newTick)
    {
        currentObjectTick = newTick;
        UpdateActiveInteractions();
    }

    private void UpdateActiveInteractions()
    {

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
                                                                  activeInteraction.interactionObject);
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
        foreach (Transform t in ObjectInteractionSpots.Keys)
        {
            if (ObjectInteractionSpots[t] == true)
                continue;
            else
            {
                ObjectInteractionSpots[t] = true;
                return t;
            }
        }
        return null;
    }

    public bool ItemHasFreeInteractionSpots()
    {
        foreach (Transform t in ObjectInteractionSpots.Keys)
        {
            if (ObjectInteractionSpots[t] == true)
                continue;
            else
                return true;
        }
        return false;
    }

    public void RegisterAsActiveInteraction(CharacterBase thisCharacter, InteractionBaseSO interactionSO, InteractableObject interactionObject)
    {
        ActiveInteraction activeInteraction = new(thisCharacter, interactionSO, currentObjectTick, interactionObject, objectInteractionSpots[0]);
        activeInteractions.Add(activeInteraction);
    }

    public void DeregisterAsActiveInteraction(CharacterBase thisCharacter, InteractionBaseSO interactionSO, InteractableObject interactionObject)
    {
        for (int i = activeInteractions.Count - 1; i >= 0; i--)
        {
            if (activeInteractions[i].interactionPefromer == thisCharacter)
            {
                if (activeInteractions[i].interactionSO == interactionSO)
                {
                    if (activeInteractions[i].interactionObject == interactionObject)
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
        foreach (Transform t in ObjectInteractionSpots.Keys)
        {
            if (t == activeInteraction.interactionSpot)
            {
                ObjectInteractionSpots[t] = false;
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
        return (interactionStartTicks + interactionLenght) - currentObjectTick;
    }

    public void DisableInteraction(InteractionBaseSO interaction)
    {
        interaction.InteractionEnabled = false;
    }

    public void DisableAllInteractions()
    {
        foreach (InteractionBaseSO interaction in ObjectInteractions)
        {
            interaction.InteractionEnabled = false;
        }
    }

    public void EnableInteraction(InteractionBaseSO interaction)
    {
        interaction.InteractionEnabled = true;
    }

    public void EnableAllInteractions()
    {
        foreach (InteractionBaseSO interaction in ObjectInteractions)
        {
            interaction.InteractionEnabled = true;
        }
    }

    //STATE
    public virtual void AddState(ObjectState_BaseSO state)
    {
        currentStates.Add(state);
        state.OnStateEnable();
        UpdateObjectInteractionValidity();
    }

    public virtual void RemoveState(ObjectState_BaseSO state)
    {
        currentStates.Remove(state);
        state.OnStateDisable();
        UpdateObjectInteractionValidity();

    }

    private void UpdateObjectInteractionValidity()
    {
        foreach (InteractionBaseSO interaction in objectInteractions)
        {
            foreach (ObjectState_BaseSO state in ObjectStates)
            {
                if (interaction.InvalidInteractionOwnerStates.Contains(state))
                    interaction.InteractionEnabled = false;
                else
                    interaction.InteractionEnabled = true;
            }
        }
    }

    protected virtual void OnDisable()
    {
        TickManager.Instance.OnTick -= CauseTick;
    }


    protected class ActiveInteraction
    {
        public CharacterBase interactionPefromer;
        public InteractionBaseSO interactionSO;
        public InteractableObject interactionObject;
        public int interactionStartTick;
        public Transform interactionSpot;
        public int interactionCurrentTick;

        public ActiveInteraction(CharacterBase _interactionPerformer, InteractionBaseSO _interactionSO, int _interactionStartTick, InteractableObject _interactionObject, Transform _interactionSpot)
        {
            this.interactionPefromer = _interactionPerformer;
            this.interactionSO = _interactionSO;
            this.interactionStartTick = _interactionStartTick;
            this.interactionObject = _interactionObject;
            this.interactionSpot = _interactionSpot;


        }
    }
}
