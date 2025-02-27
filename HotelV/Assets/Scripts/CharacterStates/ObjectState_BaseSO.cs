using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectState_BaseSO : ScriptableObject
{
    public string StateName { get => stateName; protected set => stateName = value; }
    [SerializeField]
    protected string stateName;
    public virtual void OnStateEnable()
    {

    }


    public virtual void OnStateDisable()
    {

    }
}
