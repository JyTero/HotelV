using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction
{
    public InteractionBaseSO InteractionSO;
    public string InteractionName { get; protected set; }
    public int InteractionLenght;
    public InteractableObject InteractionOwner;
    public CharacterBase InteractionInitiator;

    [HideInInspector]
    public bool InteractionEnabled = true;

    public Interaction(InteractionBaseSO interactionSO, InteractableObject interactionOwner)
    {
        InteractionSO = interactionSO;
        InteractionLenght = InteractionSO.InteractionLenghtTicks;
        InteractionName = interactionSO.InteractionName;
        InteractionOwner = interactionOwner;
    }

    public virtual void BeginInteraction(CharacterBase initiator)
    {
        InteractionInitiator = initiator;
        InteractionSO.BeginInteraction(this);
    }

    public virtual void RunInteraction(CharacterBase initiator)
    {
        InteractionSO.RunInteraction(this);
    }

    public Interaction CopyInteraction()
    {
        Interaction i = this.CloneInteraction();
        return i;
    }

    protected virtual Interaction CloneInteraction()
    {
        Interaction i = new (this.InteractionSO, this.InteractionOwner);

        i.InteractionSO = this.InteractionSO;
        i.InteractionLenght = this.InteractionSO.InteractionLenghtTicks;
        i.InteractionName = this.InteractionSO.InteractionName;
        i.InteractionOwner = this.InteractionOwner;

        return i;
    }
}

public class SocialInteraction : Interaction
{

    public SocialInteractionBaseSO SocialInteractionSO;
    public int InteractionRelationshipScoreChange;
    public SocialInteraction(InteractionBaseSO interactionSO, InteractableObject interactionOwner)
     : base(interactionSO, interactionOwner)
    {
        SocialInteractionSO = interactionSO as SocialInteractionBaseSO;
       // InteractionName = interactionSO.InteractionName;
        InteractionRelationshipScoreChange = SocialInteractionSO.InteractionRelationshipChange;
    }

    protected override Interaction CloneInteraction()
    {
        SocialInteraction socI = new(this.SocialInteractionSO, this.InteractionOwner);
        socI.InteractionName = this.InteractionName;
        socI.InteractionRelationshipScoreChange = this.InteractionRelationshipScoreChange;

        return socI;
    }
}

public class SocialResponseInteraction : SocialInteraction
{
    public int ResponderInteractionRelationshipScoreChange;
    public SocialResponseInteraction(InteractionBaseSO interactionSO, InteractableObject interactionOwner)
     : base(interactionSO, interactionOwner)
    {
        //SocialInteractionSO = interactionSO as SocialInteractionBaseSO;
        InteractionName = $"Respond to {interactionSO.InteractionName}";
        //ResponderInteractionRelationshipScoreChange = SocialInteractionSO.InteractionRelationshipChange;
    }

    public override void BeginInteraction(CharacterBase initiator)
    {
        InteractionInitiator = initiator;
        SocialInteractionSO.ResponseBeginInteraction((CharacterBase)InteractionOwner, initiator);
    }

    public override void RunInteraction(CharacterBase initiator)
    {
        SocialInteractionSO.ResponseRunInteraction((CharacterBase)InteractionOwner, initiator);
    }

    protected override Interaction CloneInteraction()
    {
        SocialResponseInteraction socI = new(this.InteractionSO, this.InteractionOwner);
        socI.InteractionName = this.InteractionName;
        socI.InteractionRelationshipScoreChange = this.InteractionRelationshipScoreChange;

        return socI;    
    }
}