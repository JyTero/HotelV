using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected CharacterNeedsManager selectedNeedsManager;
    protected CharacterBase selectedCharacter;
    protected CharacterBase panelLoadedFor;
    [Header("DEBUG")]
    public bool debugEnabled;
    protected string s = "";

    protected virtual void Awake()
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
