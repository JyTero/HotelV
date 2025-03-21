//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "SocialInteractionResponseSO", menuName = "ScriptableObjects/Interactions/SocialInteractionResponseSO")]

//public class SocialInteractionResponseSO : SocialInteractionBaseSO
//{
//    public override void BeginInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
//    {
//        base.BeginInteraction(thisCharacter, interactionOwner);

//        thisCharacter.AddState(objectStatesSO.SocialState);
//        ((CharacterBase)interactionOwner).InteractionTargetReady();
//    }
//    public override void RunInteraction(CharacterBase thisCharacter, InteractableObject interactionOwner)
//    {
//        base.RunInteraction(thisCharacter, interactionOwner);

//        interactionOwner.RegisterAsActiveInteraction(thisCharacter, this, interactionOwner);

//    }
//    public override void OnInteractionTick(CharacterBase thisCharacter, InteractableObject interactionOwner)
//    {
//        base.OnInteractionTick(thisCharacter, interactionOwner);

//        //Commented out as social need is not implemented 
//        //foreach (NeedRateChangePairs needPair in needSONeedAdjustRates)
//        //{

//        //    float needChangePerTick = NeedChangePerTick(needPair.needChangePerSecond, TickManager.Instance.TickRate);

//        //    thisCharacter.thisCharacterNeedsManager.AdjustNeed(needPair.needSO, (int)needChangePerTick);
//        //}
//    }

//    public override void OnInteractionEnd(CharacterBase thisCharacter, InteractableObject interactionOwner)
//    {
//        //thisCharacter.RemoveState(objectStatesSO.SocialState);

//        base.OnInteractionEnd(thisCharacter, interactionOwner);

//    }
//}
