using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitsPanel_UI : UIPanel
{
    [SerializeField]
    private GameObject traitItemPrefab;


    private List<TraitUIGroup> activeTraitItems = new();
    private List<TraitUIGroup> traitItemsPool = new();


    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < 5; i++)
        {
            TraitUIGroup group = new(Instantiate(traitItemPrefab), null);
            traitItemsPool.Add(group);
            group.TraitParentGO.SetActive(false);
        }
    }

    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
        selectedCharacter.thisCharacterTraitsManager.OnTraitsChange += ForceTraitPanelRefresh;
        UpdateTraitsPanel();
    }

    public override void OnPanelDeactivation()
    {
        selectedCharacter.thisCharacterTraitsManager.OnTraitsChange -= ForceTraitPanelRefresh;
        base.OnPanelDeactivation();

    }

    //void Update()
    //{
    //    if (selectedCharacter == null)
    //        return;
    //    UpdateTraitsPanel();
    //}



    private void UpdateTraitsPanel()
    {
        if (panelLoadedFor == selectedCharacter)
            return;
        else
        {
            ForceTraitPanelRefresh();
        }
    }

    private void ForceTraitPanelRefresh()
    {
        ClearTraitPanel();
        PopulateTraitPanel();

        LayoutGroup layout = GetComponent<VerticalLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());

        panelLoadedFor = selectedCharacter;
    }

    private void ClearTraitPanel()
    {
        foreach (TraitUIGroup uiGroup in activeTraitItems)
        {
            uiGroup.TraitParentGO.SetActive(false);
        }
        activeTraitItems.Clear();
    }

    private void PopulateTraitPanel()
    {
        int i = 0;
        foreach (TraitBaseSO trait in selectedCharacter.thisCharacterTraitsManager.CharacterTraits)
        {
            TraitUIGroup uiGroup;
            if (i >= selectedCharacter.thisCharacterTraitsManager.CharacterTraits.Count)
            {
                uiGroup = new(Instantiate(traitItemPrefab), null);
                traitItemsPool.Add(uiGroup);
            }
            else
                uiGroup = traitItemsPool[i];

            uiGroup.TraitSO = trait;
            uiGroup.TraitNameTMP.text = trait.TraitName;

            uiGroup.TraitParentGO.transform.SetParent(this.gameObject.transform, false);
            uiGroup.TraitParentGO.SetActive(true);

            activeTraitItems.Add(uiGroup);

            i++;
        }
    }

    private class TraitUIGroup
    {
        public GameObject TraitParentGO;
        public TMP_Text TraitNameTMP;
        public TraitBaseSO TraitSO;

        public TraitUIGroup(GameObject traitParentGO, TraitBaseSO traitSO)
        {
            TraitParentGO = traitParentGO;
            TraitNameTMP = TraitParentGO.transform.GetChild(0).GetComponent<TMP_Text>();
            TraitSO = traitSO;
        }
    }
}
