using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipPanel_UI : UIPanel
{
    [SerializeField]
    private GameObject relationshipItemPrefab;
    [SerializeField]
    private TMP_Text totalRelations;

    private List<RelationshipUIGroup> activeRelationshipItems = new();
    private List<RelationshipUIGroup> relationshipItemsPool = new();

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < 5; i++)
        {
            RelationshipUIGroup group = new(Instantiate(relationshipItemPrefab), null);
            relationshipItemsPool.Add(group);
            group.RelationshipItemParentGO.SetActive(false);
        }
    }
    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
    }

    void Update()
    {
        totalRelations.text = selectedCharacter.thisCharacterRelationshipsManager.CharacterRelationships.Count.ToString();

        if (selectedCharacter == null)
            return;
        UpdateRelationshipsPanel();
    }
    private void UpdateRelationshipsPanel()
    {
        if (panelLoadedFor == selectedCharacter)
            return;
        else
        {
            ForceRelationshipPanelRefresh();
        }
    }

    private void ForceRelationshipPanelRefresh()
    {
        ClearRelationshipsPanel();
        PopulateRelationshipPanel();

        LayoutGroup layout = GetComponent<VerticalLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout.GetComponent<RectTransform>());

        panelLoadedFor = selectedCharacter;
    }

    private void ClearRelationshipsPanel()
    {
        foreach(RelationshipUIGroup uiGroup in activeRelationshipItems)
        {
            uiGroup.RelationshipItemParentGO.SetActive(false);
        }
        activeRelationshipItems.Clear();
    }

    private void PopulateRelationshipPanel()
    {
        int i = 0;
        foreach(CharacterRelationship relationship in selectedCharacter.thisCharacterRelationshipsManager.CharacterRelationships)
        {
            RelationshipUIGroup uiGroup;
            if (i >= selectedCharacter.thisCharacterRelationshipsManager.CharacterRelationships.Count)
            {
                uiGroup = new(Instantiate(relationshipItemPrefab), null);
                relationshipItemsPool.Add(uiGroup);
            }
            else
                uiGroup = relationshipItemsPool[i];

            uiGroup.Relationship = relationship;
            uiGroup.RelationshipName.text = relationship.relationshipTarget.ObjectName;
            uiGroup.RelationshipScore.text = relationship.relationshopScore.ToString();
            uiGroup.RelationshipItemParentGO.transform.SetParent(this.gameObject.transform);
            uiGroup.RelationshipItemParentGO.SetActive(true);

            activeRelationshipItems.Add(uiGroup);

            i++;
        }
    }

    private class RelationshipUIGroup
    {
        public GameObject RelationshipItemParentGO;
        public TMP_Text RelationshipName;
        public TMP_Text RelationshipScore;
        public CharacterRelationship Relationship;

        public RelationshipUIGroup(GameObject relationItemparent, CharacterRelationship relationship)
        {
            RelationshipItemParentGO = relationItemparent;
            RelationshipName = RelationshipItemParentGO.transform.GetChild(0).GetComponent<TMP_Text>();
            RelationshipScore = RelationshipItemParentGO.transform.GetChild(1).GetComponent<TMP_Text>();
            Relationship = relationship;
        }

    }
}
