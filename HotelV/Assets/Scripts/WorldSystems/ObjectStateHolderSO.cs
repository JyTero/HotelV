using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectStatesSO", menuName = "ScriptableObjects/Tools/ObjectStatesSO")]
public class ObjectStateHolderSO : ScriptableObject 
{
    //Keeps refrence to all states InteractableObjects can have

    //States
    public ObjectState_IdleSO IdleState { get => idleState; private set => idleState = value; }
    [SerializeField]
    private ObjectState_IdleSO idleState;
    
    public ObjectState_BusySO BusyState { get => busyState; private set => busyState = value; }
    [SerializeField]
    private ObjectState_BusySO busyState;
    
    public ObjectState_AsleepSO SleepState { get => sleepState; private set => sleepState = value; }
    [SerializeField]
    private ObjectState_AsleepSO sleepState;

    public ObjectState_SocializingSO SocialState { get => socialState; private set => socialState = value; }
    [SerializeField]
    private ObjectState_SocializingSO socialState;
}
