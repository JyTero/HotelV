using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeedsPanel_UI : MonoBehaviour
{
    [SerializeField]
    private ProgressBar hungerNeedBar;
    [SerializeField]
    TMP_Text hungerNeedNumber;

    [SerializeField]
    private ProgressBar energyNeedBar;
    [SerializeField]
    TMP_Text energyNeedNumber;
    //TODO: Make logic to get selected character, refresh UI on character select?
    [SerializeField]
    private CharacterNeedsManager selectedNeedsManager;


    // Update is called once per frame
    void Update()
    {
        UpdateHunger();
        UpdateEnergy();
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
}
