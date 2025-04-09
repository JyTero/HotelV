using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


[RequireComponent(typeof(CharacterNavigation))]
[RequireComponent(typeof(UtilityAI))]
public class CharacterBase : InteractableObject
{
    [HideInInspector]
    public CharacterNeedsManager thisCharacterNeedsManager;
    [HideInInspector]
    public CharacterTraitsManager thisCharacterTraitsManager;
    [HideInInspector]
    public CharacterRelationshipManager thisCharacterRelationshipsManager;

    [SerializeField]
    [Tooltip("Idle time in ticks before new interaction search begins")]
    private int idleTimer;
    private int idleTimeStart = -1;


    private UtilityAI UtilityAI;
    private Interaction currentInteraction;
    private List<Interaction> queuedInteractions = new();
    private CharacterNavigation thisCharacterNavigation;

    private Material defaultCharacterMat;

    private TraitBaseSO newTrait;

    protected override void Start()
    {
        base.Start();
        UtilityAI = GetComponent<UtilityAI>();
        thisCharacterNavigation = GetComponent<CharacterNavigation>();
        thisCharacterNeedsManager = GetComponent<CharacterNeedsManager>();
        thisCharacterTraitsManager = GetComponent<CharacterTraitsManager>();
        thisCharacterRelationshipsManager = GetComponent<CharacterRelationshipManager>();

        BeIdle();
    }

    //DEBUG
    private float hungerDecrease = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartUtilityAI();
        }
    }

    public void StartUtilityAI()
    {
        idleTimeStart = -1;

        currentInteraction = UtilityAI.ChooseWhatToDo(this, thisCharacterNeedsManager);

        RunInteraction(currentInteraction);

    }

    private void RunInteraction(Interaction interaction)
    {
        // currentInteraction = interaction;
        currentInteraction = interaction.CopyInteraction();
        currentInteraction = thisCharacterTraitsManager.ModifyInteractionByTrait(currentInteraction);
        RemoveState(objectStatesSO.IdleState);

        if (currentInteraction.InteractionSO is EarnTrait_InteractionSO getTraitSO)
        {
            currentInteraction.InteractionInitiator = this;
            getTraitSO.BeginInteraction(currentInteraction, newTrait);
            newTrait = null;
        }
        else
            currentInteraction.BeginInteraction(this);
    }



    public void SetDestination(Vector3 destination)
    {
        thisCharacterNavigation.SetAndSaveDestination(destination);
    }

    //Seperate when arriving to interaction destination and when other sort of destination
    public void OnAtDestination()
    {
        currentInteraction.RunInteraction(this);
    }

    public void OnInteractionEnd()
    {
        currentInteraction = null;

        if (QueuedInteraction() != null)
        {
            RunQueuedInteraction();
        }
        else
        {
            AddState(objectStatesSO.IdleState);
            BeIdle();
        }

    }

    private void BeIdle()
    {
        idleTimeStart = -1;
        AddState(objectStatesSO.IdleState);
        TickManager.Instance.OnTick += IdleTick;

    }
    private void IdleTick(int currentTick)
    {
        if (currentInteraction != null)
        {
            TickManager.Instance.OnTick -= IdleTick;
            RunInteraction(currentInteraction);
            return;
        }
        if (QueuedInteraction() != null)
        {
            RunQueuedInteraction();

            TickManager.Instance.OnTick -= IdleTick;
            return;
        }

        if (idleTimeStart == -1)
        {
            idleTimeStart = currentTick;
            return;
        }
        else
        {
            if (idleTimeStart + idleTimer > currentTick)
            {
                return;
            }
            else
            {
                StartUtilityAI();
                TickManager.Instance.OnTick -= IdleTick;
            }
        }
    }

    private void WaitTargetSocialIdleTick(int currentTick)
    {
        if (currentInteraction.InteractionOwner.ObjectStates.Contains(objectStatesSO.SocialState))
        {
            TickManager.Instance.OnTick -= WaitTargetSocialIdleTick;
            currentInteraction.BeginInteraction(this);
            //StartSocialInteractionRecieverInteration();fgh
            //currentInteraction.InteractionSO.BeginInteraction(this, currentInteraction.InteractableObject);
        }
        else
        {

        }
    }

    public override void AddState(ObjectState_BaseSO newState)
    {
        base.AddState(newState);
    }

    public override void RemoveState(ObjectState_BaseSO newState)
    {
        base.RemoveState(newState);
    }

    public void PrepareToBeSocialTarget(InteractionBaseSO socialInteractionSO, InteractableObject interactionInitiator)
    {
        SocialResponseInteraction beChattedTo = new(socialInteractionSO, interactionInitiator);
        queuedInteractions.Add(beChattedTo);
    }

    public void AddInteractionToQueue(InteractionBaseSO interactionSO, InteractableObject interactionOwner)
    {
        Interaction i = new((interactionOwner.ObjectInteractions.FirstOrDefault(inter => inter.InteractionSO == interactionSO).InteractionSO),
                             interactionOwner);
        queuedInteractions.Add(i);
    }

    public void AddTrait(TraitBaseSO trait)
    {
        newTrait = trait;
        Interaction getTraitInteraction = ObjectInteractions.FirstOrDefault(inter => inter.InteractionSO is EarnTrait_InteractionSO);
        queuedInteractions.Add(getTraitInteraction);
    }

    private void RunQueuedInteraction()
    {
        //Debug.LogWarning($"Tried to run queued interaction, this shouldn't happen ({objectName} | {queueInteraction.InteractionSO.InteractionName})");


        currentInteraction = queuedInteractions[0];
        queuedInteractions.RemoveAt(0);
        RunInteraction(currentInteraction);

    }

    public void InteractionTargetReady()
    {
        ((SocialInteractionBaseSO)currentInteraction.InteractionSO).ContinueInteractionOnTargetReady(currentInteraction.InteractionInitiator, currentInteraction.InteractionOwner);
    }

    public void ChangCharacterMaterial(Material newMat)
    {
        var mr = GetComponent<MeshRenderer>();
        defaultCharacterMat = mr.material;
        mr.material = newMat;
    }

    public void RestoreDefaultMateria()
    {
        var mr = GetComponent<MeshRenderer>();
        mr.material = defaultCharacterMat;

    }

    public Interaction CurrentInteraction()
    {
        return currentInteraction;
    }

    public Interaction QueuedInteraction()
    {
        if (queuedInteractions.Count > 0)
            return queuedInteractions[0];
        else
            return null;
    }
    public List<Interaction> QueuedInteractionsList()
    {
        return queuedInteractions;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TickManager.Instance.OnTick -= IdleTick;
    }
}
