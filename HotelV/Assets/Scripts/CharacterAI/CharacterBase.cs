using System.Collections;
using System.Collections.Generic;
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


    [SerializeField]
    [Tooltip("Idle time in ticks before new interaction search begins")]
    private int idleTimer;
    private int idleTimeStart = -1;


    private UtilityAI UtilityAI;
    private InteractionInScoring currentInteraction;
    private InteractionInScoring queueInteraction;
    private CharacterNavigation thisCharacterNavigation;

    private Material defaultCharacterMat;


    protected override void Start()
    {
        base.Start();
        UtilityAI = GetComponent<UtilityAI>();
        thisCharacterNavigation = GetComponent<CharacterNavigation>();
        thisCharacterNeedsManager = GetComponent<CharacterNeedsManager>();
        thisCharacterTraitsManager = GetComponent<CharacterTraitsManager>();

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

        InteractionInScoring interaction = UtilityAI.ChooseWhatToDo(this, thisCharacterNeedsManager);


        RunInteraction(interaction);


    }

    private void RunInteraction(InteractionInScoring interaction)
    {
        currentInteraction = interaction;
        RemoveState(objectStatesSO.IdleState);
        interaction.InteractionSO.BeginInteraction(this, interaction.InteractableObject);
    }

    public void SetDestination(Vector3 destination)
    {
        thisCharacterNavigation.SetAndSaveDestination(destination);
    }

    public void OnAtDestination()
    {
        currentInteraction.InteractionSO.StartInteraction(this, currentInteraction.InteractableObject);
    }

    public void OnInteractionEnd()
    {
        currentInteraction = null;

        if (queueInteraction != null)
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
        if (queueInteraction != null)
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

    public void WaitInteractionTargetToHaveSocialState(InteractableObject interactionTarget)
    {
        TickManager.Instance.OnTick += WaitTargetSocialIdleTick;
    }

    private void WaitTargetSocialIdleTick(int currentTick)
    {
        if (currentInteraction.InteractableObject.ObjectStates.Contains(objectStatesSO.SocialState))
        {
            TickManager.Instance.OnTick -= WaitTargetSocialIdleTick;
            currentInteraction.InteractionSO.BeginInteraction(this, currentInteraction.InteractableObject);
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

    public void PrepareToBeSocialTarget(InteractionBaseSO socialRecieverInteractionSO, InteractableObject interactionInitiator)
    {
        InteractionInScoring beChattedTo = new(socialRecieverInteractionSO, interactionInitiator);
        if (currentInteraction == null)
            currentInteraction = beChattedTo;
        else
            queueInteraction = beChattedTo;

    }

    public void StartSocialInteractionRecieverInteration()
    {
        if (currentInteraction.InteractionSO is BeChattedWith_InteractionSO)
            RunInteraction(currentInteraction);
        else if (queueInteraction.InteractionSO is BeChattedWith_InteractionSO)
        {
            RunQueuedInteraction();
        }
        else
            Debug.LogWarning("No valid social response interaction found on " + objectName);
    }

    private void RunQueuedInteraction()
    {
        currentInteraction = queueInteraction;
        queueInteraction = null;
        RunInteraction(currentInteraction);

    }


    public void ChangCharacterMaterialByTrait(TraitBaseSO trait)
    {
        var mr = GetComponent<MeshRenderer>();
        defaultCharacterMat = mr.material;
        mr.material = trait.TraitCharacterAppearanceMaterial;
    }

    public void RestoreDefaultMateria()
    {
        var mr = GetComponent<MeshRenderer>();
        mr.material = defaultCharacterMat;

    }

    public InteractionInScoring CurrentInteraction()
    {
        return currentInteraction;
    }

    public InteractionInScoring QueuedInteraction()
    {
        return queueInteraction;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        TickManager.Instance.OnTick -= IdleTick;
    }
}
