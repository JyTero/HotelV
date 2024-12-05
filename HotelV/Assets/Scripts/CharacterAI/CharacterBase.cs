using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterNavigation))]
[RequireComponent(typeof(UtilityAI))]
public class CharacterBase : MonoBehaviour
{

    public List<NeedBaseSO> CharacterNeeds { get => characterNeeds; private set => characterNeeds = value; }
    [SerializeField]
    private List<NeedBaseSO> characterNeeds;


}
