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
    private TMP_Text charCurrentStateTMP;


    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
    }


    private void Update()
    {
        if(selectedCharacter == null)
            return;
        charNameTMP.text = selectedCharacter.CharacterName;
        charCurrentInteractionTMP.text = selectedCharacter.CurrentInteraction().InteractionName;

    }
    public override void OnPanelDeactivation()
    {
        base.OnPanelDeactivation();
    }
}
