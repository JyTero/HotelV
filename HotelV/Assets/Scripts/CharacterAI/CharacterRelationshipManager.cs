using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterRelationshipManager : MonoBehaviour
{

    public HashSet<CharacterRelationship> CharacterRelationships { get; private set; }

    private void Awake()
    {
        CharacterRelationships = new();
    }

    public void ModifyRelationship(CharacterBase relationshipTarget, int relationChange)
    {
        CharacterRelationship relationship = CharacterRelationships.FirstOrDefault(x => x.relationshipTarget == relationshipTarget);

        if (relationship == null)
            CreateNewRelationship(relationshipTarget, relationChange);
        else
            AdjustRelationshipValue(relationship, relationChange);
    }

    private void CreateNewRelationship(CharacterBase relationshipTarget, int relationChange)
    {
        CharacterRelationships.Add(new(relationshipTarget, relationChange));
    }
    private void AdjustRelationshipValue(CharacterRelationship relationship, int relationChange)
    {
        relationship.AdjustRelationshipValue(relationship, relationChange);
    }
}

public class CharacterRelationship
{
    public CharacterBase relationshipTarget { get; private set; }
    public int relationshopScore { get; private set; }
    // relationshop tags (Friend, lover, childe, enemy...

    public CharacterRelationship(CharacterBase relTarget, int relScore)
    {
        relationshipTarget = relTarget;
        relationshopScore = relScore;
    }

    public void AdjustRelationshipValue(CharacterRelationship relationship, int relationChange)
    {
        relationship.relationshopScore += relationChange;
    }
}
