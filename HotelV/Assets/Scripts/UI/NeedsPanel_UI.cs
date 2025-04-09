using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class NeedsPanel_UI : UIPanel
{
    [Header("NeedsPanel")]
    [SerializeField]
    private int needHeightOffset = -32;

    [SerializeField]
    private NeedSOHolderSO needHolderSO;


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

    private List<NeedUIGroup> activeNeedItems = new();
    private List<NeedUIGroup> needItemsPool = new();

    [SerializeField]
    private GameObject needItemPrefab;

    protected void Start()
    {

        for (int i = 0; i < needHolderSO.NeedsHash.Count(); i++)
        {
            NeedUIGroup group = new(Instantiate(needItemPrefab), null);
            needItemsPool.Add(group);
            group.needParentGO.SetActive(false);
        }
    }

    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
    }

    void Update()
    {
        if (selectedCharacter == null)
            return;
        UpdateNeedsPanel();
        UpdateActiveNeeds();
    }


    private void UpdateActiveNeeds()
    {
        foreach (NeedBase need in selectedNeedsManager.characterNeeds)
        {

            NeedUIGroup group = activeNeedItems.FirstOrDefault(grp => grp.NeedRelatedSO == need.needSO);
            if (group == null)
            {
                string s = "Failed need update!\n";
                foreach (NeedBase need2 in selectedNeedsManager.characterNeeds)
                {
                    foreach (NeedUIGroup group2 in activeNeedItems)
                    {
                        s += $"Need {need2.needSO.NeedName} and group {group2.NeedRelatedSO.NeedName} dont match\n";

                    }
                }

                Debug.LogError(s);
            }
            else
                UpdateNeedValues(group, need);

        }
    }

    public override void OnPanelDeactivation()
    {
        base.OnPanelDeactivation();
    }
    private void UpdateNeedsPanel()
    {
        if (panelLoadedFor == selectedCharacter)
            return;
        else
        {
            ForceNeedsPanelRefresh();
        }
    }
    public void ForceNeedsPanelRefresh()
    {
        DisableAllNeedsOnPanel();
        EnableValidNeedsOnPanel();
        LayoutGroup layout = GetComponent<VerticalLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());

        panelLoadedFor = selectedCharacter;

        if (debugEnabled)
            Debug.Log(s);
    }


    private void DisableAllNeedsOnPanel()
    {
        foreach (NeedUIGroup group in activeNeedItems)
        {
            group.needParentGO.SetActive(false);
            if (debugEnabled)
                s += $"NeedUIGroup disabled: {group.NeedRelatedSO.NeedName}\n";
            needItemsPool.Add(group);
        }
        activeNeedItems.Clear();

    }

    private void EnableValidNeedsOnPanel()
    {

        foreach (NeedBase need in selectedCharacter.thisCharacterNeedsManager.characterNeeds)
        {
            NeedUIGroup group = needItemsPool[0];
            needItemsPool.RemoveAt(0);

            group.NeedRelatedSO = need.needSO;
            group.NeedName.text = need.needSO.NeedName;
            group.needParentGO.transform.SetParent(this.gameObject.transform, false);
            group.needParentGO.SetActive(true);

            activeNeedItems.Add(group);

            UpdateNeedValues(group, need);
        }

    }

    private void UpdateNeedValues(NeedUIGroup group, NeedBase need)
    {
        group.needBar.current = need.needValue;
        group.NeedNumber.text = need.needValue.ToString();
    }

    //In the future, add proper helper class to store needUI data for simpler expansion and retooling.
    private class NeedUIGroup
    {
        public GameObject needParentGO;
        public ProgressBar needBar;
        public TMP_Text NeedNumber;
        public TMP_Text NeedName;
        public NeedBaseSO NeedRelatedSO;


        public NeedUIGroup(GameObject _needParentGO, NeedBaseSO _needRelatedSO)
        {
            needParentGO = _needParentGO;
            NeedRelatedSO = _needRelatedSO;
            needBar = needParentGO.transform.GetChild(2).GetComponent<ProgressBar>();
            NeedNumber = needParentGO.transform.GetChild(1).GetComponent<TMP_Text>();
            NeedName = needParentGO.transform.GetChild(0).GetComponent<TMP_Text>();
        }

    }
}
