using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject characterUI;

    private GameObject currentActiveUIView;
    private NeedsPanel_UI needsPanelUI;
    private CharacterDebug_UI characterDebugUI;
    private RelationshipPanel_UI relationsipUI;

    private void Start()
    {
        needsPanelUI = FindObjectOfType<NeedsPanel_UI>();
        characterDebugUI = FindObjectOfType<CharacterDebug_UI>();
        relationsipUI = FindAnyObjectByType<RelationshipPanel_UI>();

        characterUI.SetActive(false);

    }

    public void EnableCharacterUI(CharacterBase character)
    {
        if (currentActiveUIView != null)
        {
            currentActiveUIView.SetActive(false);
        }

        needsPanelUI.OnPanelActivation(character);
        characterDebugUI.OnPanelActivation(character);
        relationsipUI.OnPanelActivation(character);

        characterUI.SetActive(true);
        currentActiveUIView = characterUI;
    }

    public void DisableCharacterUI()
    {
        currentActiveUIView = null;
        characterUI.SetActive(false);
    }
}
