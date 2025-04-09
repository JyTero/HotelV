using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talkative_TraitSO", menuName = "ScriptableObjects/Traits/Talkative_TraitSO")]
public class Talkative_TraitSO : TraitBaseSO
{

    [SerializeField]
    private float socialInteractionLenghtMultiplier = 1.5f;
    [SerializeField]
    private float relationshipScoreAdjustMultiplier = 1.5f;

    public override void OnTraitAdd(CharacterBase thisCharacter)
    {
        base.OnTraitAdd(thisCharacter);

    }

    public override void OnTraitRemove(CharacterBase thisCharacter)
    {
        base.OnTraitRemove(thisCharacter);
    }

    public override SocialInteraction ModifyInteractionByTrait(SocialInteraction socialInteraction)
    {
       if (socialInteraction.InteractionSO.GetInteractionType() == InteractionType.Social)
        {
            float f = 0;

            //SocialInteraction socInteraction = (SocialInteraction)interaction;

            f = socialInteraction.InteractionLenght * socialInteractionLenghtMultiplier;
            Debug.Log($"Interaction ({socialInteraction.InteractionName}) Lenght, old: {socialInteraction.InteractionLenght} Vs new: {f}");

            socialInteraction.InteractionLenght = (int)f;


            f = socialInteraction.InteractionRelationshipScoreChange * relationshipScoreAdjustMultiplier;
            Debug.Log($"Interaction ({socialInteraction.InteractionName}) score change, old: {socialInteraction.InteractionRelationshipScoreChange} Vs new: {f}");

            socialInteraction.InteractionRelationshipScoreChange = (int)f;

            return socialInteraction;
        }
        else
        {
            Debug.LogError("Non Social InteractionType is SocialInteraction!" + socialInteraction.InteractionName);
            return null;
        }
    }

}
