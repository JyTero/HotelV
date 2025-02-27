using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDebug_UI : UIPanel
{
    [SerializeField]
    private TMP_Text charNameTMP;
    [SerializeField]
    private TMP_Text charCurrentInteractionTMP;
    [SerializeField]
    private TMP_Text charQueuedInteractionTMP;
    [SerializeField]
    private TMP_Text charCurrentStateTMP;


    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
    }

    void Update()
    {
        if (selectedCharacter == null)
            return;
        UpdateCharName();
        UpdateCharCurrentInteraction();
        UpdateCharQueuedInteraction();
        UpdateCharCurrentState();
    }

    private void UpdateCharName()
    {
        charNameTMP.text = selectedCharacter.ObjectName;

    }
    private void UpdateCharCurrentInteraction()
    {
        if (selectedCharacter.CurrentInteraction() == null)
            charCurrentInteractionTMP.text = "null";
        else
            charCurrentInteractionTMP.text = selectedCharacter.CurrentInteraction().InteractionSO.InteractionName;
    }
    private void UpdateCharQueuedInteraction()
    {
        var queuedInteraction = selectedCharacter.QueuedInteraction();
        if (queuedInteraction == null)
            charQueuedInteractionTMP.text = "null";
        else
            charQueuedInteractionTMP.text = queuedInteraction.InteractionSO.InteractionName;
    }
    private void UpdateCharCurrentState()
    {
        if (selectedCharacter.ObjectStates.Count == 0)
            charCurrentStateTMP.text = "null";
        else
        {
            string s = "";
            foreach (ObjectState_BaseSO state in selectedCharacter.ObjectStates)
            {
                s += state.StateName + "\n";
            }
            charCurrentStateTMP.text = s;
        }
    }
    public override void OnPanelDeactivation()
    {
        base.OnPanelDeactivation();
    }
}
