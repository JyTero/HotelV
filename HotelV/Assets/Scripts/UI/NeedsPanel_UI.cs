using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NeedsPanel_UI : UIPanel
{
    [Header("NeedsPanel")]
    [SerializeField]
    private int needHeightOffset = -32;

    [SerializeField]
    private ProgressBar hungerNeedBar;
    [SerializeField]
    private TMP_Text hungerNeedNumber;
    private GameObject hungerParentGO;

    [SerializeField]
    private ProgressBar energyNeedBar;
    [SerializeField]
    private TMP_Text energyNeedNumber;
    private GameObject energyParentGO;

    [SerializeField]
    private ProgressBar funNeedBar;
    [SerializeField]
    private TMP_Text funNeedNumber;
    private GameObject funParentGO;

    [SerializeField]
    private ProgressBar bloodNeedBar;
    [SerializeField]
    private TMP_Text bloodNeedNumber;
    private GameObject bloodParentGO;

    [SerializeField]
    private Hunger_NeedSO hungerSO;
    [SerializeField]
    private Energy_NeedSO energySO;
    [SerializeField]
    private Fun_NeedSO funSO;
    [SerializeField]
    private Blood_NeedSO bloodSO;



    private Dictionary<NeedBaseSO, GameObject> needSOneedParentPairs = new();
    private HashSet<NeedUIGroup> needUIGroups = new();

    private CharacterBase panelLoadedFor;
    protected override void Awake()
    {
        base.Awake();
        hungerParentGO = hungerNeedBar.transform.parent.gameObject;
        needSOneedParentPairs.Add(hungerSO, hungerParentGO);
        needUIGroups.Add(new NeedUIGroup(hungerParentGO, hungerSO));

        energyParentGO = energyNeedBar.transform.parent.gameObject;
        needSOneedParentPairs.Add(energySO, energyParentGO);
        needUIGroups.Add(new NeedUIGroup(energyParentGO, energySO));

        funParentGO = funNeedBar.transform.parent.gameObject;
        needSOneedParentPairs.Add(funSO, funParentGO);
        needUIGroups.Add(new NeedUIGroup(funParentGO, funSO));

        bloodParentGO = bloodNeedBar.transform.parent.gameObject;
        needSOneedParentPairs.Add(bloodSO, bloodParentGO);
        needUIGroups.Add(new NeedUIGroup(bloodParentGO, bloodSO));
    }

    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
          //  RefreshNeedsPanel();

    }

    void Update()
    {
        if (selectedCharacter == null)
            return;
        RefreshNeedsPanel();
        UpdateActiveNeeds();


        //UpdateHunger();
        //UpdateEnergy();
        //UpdateFun();
    }

    private void UpdateHunger()
    {
        int value = selectedNeedsManager.HungerNeedValue;

        hungerNeedBar.current = value;
        hungerNeedNumber.text = value.ToString();
    }

    private void UpdateEnergy()
    {
        int value = selectedNeedsManager.EnergyNeedValue;

        energyNeedBar.current = value;
        energyNeedNumber.text = value.ToString();
    }

    private void UpdateFun()
    {
        int value = selectedNeedsManager.FunNeedValue;

        funNeedBar.current = value;
        funNeedNumber.text = value.ToString();
    }

    private void UpdateActiveNeeds()
    {
        foreach (NeedBase need in selectedNeedsManager.characterNeeds)
        {

            foreach (NeedUIGroup uiGroup in needUIGroups)
            {
                if (!uiGroup.needParentGO.activeInHierarchy)
                    continue;
                if (uiGroup.needRelatedSO == need.needSO)
                {
                    uiGroup.needBar.current = need.needValue;
                    uiGroup.needNumber.text = need.needValue.ToString();
                }
            }
        }
    }

    public override void OnPanelDeactivation()
    {
        base.OnPanelDeactivation();
    }

    public void ForceNeedsPanelRefresh()
    {
        DisableAllNeedsOnPanel();
        EnableValidNeedsOnPanel();
        LayoutGroup layout = GetComponent<VerticalLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
        //  RepositionNeedsOnPanel();
        panelLoadedFor = selectedCharacter;

        if (debugEnabled)
            Debug.Log(s);
    }
    private void RefreshNeedsPanel()
    {
        if (panelLoadedFor == selectedCharacter)
            return;
        else
        {
            DisableAllNeedsOnPanel();
            EnableValidNeedsOnPanel();
            LayoutGroup layout = GetComponent<VerticalLayoutGroup>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());
            //  RepositionNeedsOnPanel();
            panelLoadedFor = selectedCharacter;

            if (debugEnabled)
                Debug.Log(s);
        }
    }

    private void DisableAllNeedsOnPanel()
    {
        foreach (NeedUIGroup uiGroup in needUIGroups)
        {
            if (uiGroup.needParentGO.activeInHierarchy)
                uiGroup.needParentGO.SetActive(false);
            if (debugEnabled)
                s += $"NeedUIGroup disabled: {uiGroup.needRelatedSO.NeedName}\n";
        }
    }

    private void EnableValidNeedsOnPanel()
    {
        foreach (NeedBase need in selectedCharacter.thisCharacterNeedsManager.characterNeeds)
        {
            GameObject parent;
            if (needSOneedParentPairs.ContainsKey(need.needSO))
            {
                needSOneedParentPairs.TryGetValue(need.needSO, out parent);
                parent.SetActive(true);
                s += $"NeedUIGroup enabled: {need.needSO.NeedName}\n";

            }
            else
                Debug.LogError($"UI NeedsPanel EnableValidNeedsOnPanel failed to find need {need.needSO.NeedName} from its needSOParentPairs!");
        }
    }
    private void RepositionNeedsOnPanel()
    {
        int placedNeeds = 1;
        int newNeedHeight = 0;
        foreach (NeedUIGroup uiGroup in needUIGroups)
        {
            if (!uiGroup.needParentGO.activeInHierarchy)
                continue;
            else
            {
                newNeedHeight = placedNeeds * needHeightOffset;
                //uiGroup.needParentGO.GetComponent<RectTransform>().pivot
                uiGroup.needParentGO.GetComponent<RectTransform>().position = new Vector2(0, newNeedHeight);
                placedNeeds++;
            }
        }
    }



    //In the future, add proper helper class to store needUI data for simpler expansion and retooling.
    private class NeedUIGroup
    {
        public GameObject needParentGO;
        public ProgressBar needBar;
        public TMP_Text needNumber;
        public NeedBaseSO needRelatedSO;

        public NeedUIGroup(GameObject _needParentGO, NeedBaseSO _needRelatedSO)
        {
            needParentGO = _needParentGO;
            needRelatedSO = _needRelatedSO;
            needBar = needParentGO.transform.GetChild(2).GetComponent<ProgressBar>();
            needNumber = needParentGO.transform.GetChild(1).GetComponent<TMP_Text>();
        }

    }
}
