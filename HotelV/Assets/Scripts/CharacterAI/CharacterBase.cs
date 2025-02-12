using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterNavigation))]
[RequireComponent(typeof(UtilityAI))]
public class CharacterBase : MonoBehaviour
{
    [HideInInspector]
    public CharacterNeedsManager thisCharacterNeedsManager;

    [Header("Character information")]
    [SerializeField]
    private string characterName;
    public string CharacterName { get => characterName; private set => characterName = value; }


    [SerializeField]
    [Tooltip("Idle time in ticks before new interaction search begins")]
    private int idleTimer;
    private int idleTimeStart = -1;


    private UtilityAI UtilityAI;
    private InteractionBaseSO currentInteraction;
    private ItemBase currentInteractionItem;
    private CharacterNavigation thisCharacterNavigation;


    [Header("DEBUG")]
    [SerializeField]
    private bool debugEnabled;
    private void Start()
    {
        UtilityAI = GetComponent<UtilityAI>();
        thisCharacterNavigation = GetComponent<CharacterNavigation>();
        thisCharacterNeedsManager = GetComponent<CharacterNeedsManager>();

        TickManager.Instance.OnTick += IdleTick;
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
        TickManager.Instance.OnTick -= IdleTick;
        idleTimeStart = -1;

        InteractionInScoring interaction = UtilityAI.ChooseWhatToDo(this, thisCharacterNeedsManager);
        if (interaction != null)
        {
            interaction.InteractionSO.InitiateInteraction(this, interaction.InteractionItem);
            currentInteraction = interaction.InteractionSO;
            currentInteractionItem = interaction.InteractionItem;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        thisCharacterNavigation.SetAndSaveDestination(destination);
    }

    public void OnAtDestination()
    {
        currentInteraction.StartInteraction(this, currentInteractionItem);
    }

    public void OnInteractionEnd()
    {
        currentInteraction = null;
        currentInteractionItem = null;
        TickManager.Instance.OnTick += IdleTick;
    }

    private void IdleTick(int currentTick)
    {
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
                if (debugEnabled)
                    Debug.Log($"{characterName} begins idle UtilityAI");
                StartUtilityAI();
            }
        }
    }

    public InteractionBaseSO CurrentInteraction()
    {
        return currentInteraction;
    }
    public ItemBase CurrentInteractionItem()
    {
        return currentInteractionItem;
    }
    private void OnDisable()
    {
        TickManager.Instance.OnTick -= IdleTick;
    }
}
