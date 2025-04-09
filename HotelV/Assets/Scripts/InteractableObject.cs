using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public string ObjectName { get => objectName; protected set => objectName = value; }
    [SerializeField]
    protected string objectName;

    public List<Interaction> ObjectInteractions { get; private set; }

    public Dictionary<Transform, bool> ObjectInteractionSpots = new();

    //[SerializeField]
    //protected List<Interaction> objectInteractions = new();
    [SerializeField]
    protected List<Transform> objectInteractionSpots = new();

    [SerializeField]
    private List<InteractionBaseSO> interactionSOs = new();

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
        ObjectInteractions = new();
        MoveInteractionSpotsFromListToDictionary();
        IntialiseInteractionsFromSOs();
    }

    private void MoveInteractionSpotsFromListToDictionary()
    {
        foreach (Transform t in objectInteractionSpots)
        {
            ObjectInteractionSpots.Add(t, false);
        }
    }

    private void IntialiseInteractionsFromSOs()
    {
        dbString = "";

        foreach (InteractionBaseSO interactionSO in interactionSOs)
        {
            switch (interactionSO.interactionType)
            {
                case InteractionType.Social:
                    SocialInteraction socInteraction = new SocialInteraction(interactionSO, this);
                    ObjectInteractions.Add(socInteraction);
                    if (debugEnabled)
                        dbString += $"{ObjectName} initialised interaction {interactionSO.InteractionName} as SocialInteraction ({socInteraction.GetType().ToString()})\n";
                    continue;
                default:
                    Interaction interaction = new(interactionSO, this);
                    ObjectInteractions.Add(interaction);
                    if (debugEnabled)
                        dbString += $"{ObjectName} initialised interaction {interactionSO.InteractionName} as Interaction ({interaction.GetType().ToString()})\n";
                    continue;
            }

        }
        if (debugEnabled)
            Debug.Log($" {ObjectName} initialised interactions\n {dbString}");
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
                activeInteraction.interaction.InteractionSO.OnInteractionEnd(activeInteraction.interaction);
                //Mark for deregistration, deregister after iteration
                deregisterActiveInteractions.Add(activeInteraction);

            }
            else
            {
                //Update Interaction
                activeInteraction.interaction.InteractionSO.OnInteractionTick(activeInteraction.interaction);
            }
        }

        foreach (ActiveInteraction activeInteraction in deregisterActiveInteractions)
        {
            DeregisterAsActiveInteraction(activeInteraction.interaction);
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



    public void RegisterAsActiveInteraction(Interaction interaction)
    {
        ActiveInteraction activeInteraction = new(interaction, currentObjectTick, objectInteractionSpots[0]);
        activeInteractions.Add(activeInteraction);
    }

    public void DeregisterAsActiveInteraction(Interaction interaction)
    {
        for (int i = activeInteractions.Count - 1; i >= 0; i--)
        {
            if (activeInteractions[i].interaction.InteractionInitiator == interaction.InteractionInitiator)
            {
                if (activeInteractions[i].interaction.InteractionSO == interaction.InteractionSO)
                {
                    if (activeInteractions[i].interaction.InteractionOwner == interaction.InteractionOwner)
                    {
                        activeInteractions.Remove(activeInteractions[i]);
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
        int ticksRemaining = InteractionTicksRemaining(activeInteraction.interaction.InteractionLenght, activeInteraction.interactionStartTick);
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

    public void DisableInteraction(Interaction interaction)
    {
        interaction.InteractionEnabled = false;
    }

    public void DisableAllInteractions()
    {
        foreach (Interaction interaction in ObjectInteractions)
        {
            interaction.InteractionEnabled = false;
        }
    }

    public void EnableInteraction(Interaction interaction)
    {
        interaction.InteractionEnabled = true;
    }

    public void EnableAllInteractions()
    {
        foreach (Interaction interaction in ObjectInteractions)
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
        foreach (Interaction interaction in ObjectInteractions)
        {
            foreach (ObjectState_BaseSO state in ObjectStates)
            {
                if (interaction.InteractionSO.InvalidInteractionOwnerStates.Contains(state))
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
       public Interaction interaction;
        public int interactionStartTick;
        public Transform interactionSpot;
        public int interactionCurrentTick;

        public ActiveInteraction(Interaction _interaction, int _interactionStartTick, Transform _interactionSpot)
        {

            this.interactionStartTick = _interactionStartTick;
            this.interactionSpot = _interactionSpot;
            this.interaction = _interaction;


        }
    }
}
