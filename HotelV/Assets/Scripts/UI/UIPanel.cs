using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{

    
    protected CharacterNeedsManager selectedNeedsManager;
    protected CharacterBase selectedCharacter;

    public virtual void OnPanelActivation()
    {

    }
    public virtual void OnPanelActivation(CharacterBase selected)
    {
        selectedCharacter = selected;
        selectedNeedsManager = selected.GetComponent<CharacterNeedsManager>();
    }
    public virtual void  OnPanelDeactivation()
    {

    }
}
