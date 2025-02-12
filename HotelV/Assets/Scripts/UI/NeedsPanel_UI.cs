using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeedsPanel_UI : UIPanel
{
    [SerializeField]
    private ProgressBar hungerNeedBar;
    [SerializeField]
    private TMP_Text hungerNeedNumber;

    [SerializeField]
    private ProgressBar energyNeedBar;
    [SerializeField]
    private TMP_Text energyNeedNumber;

    [SerializeField]
    private ProgressBar funNeedbar;
    [SerializeField]
    private TMP_Text funNeedNumber;




    public override void OnPanelActivation(CharacterBase selectedCharacter)
    {
        base.OnPanelActivation(selectedCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHunger();
        UpdateEnergy();
        UpdateFun();
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

    private void UpdateFun()
    {
        int value = selectedNeedsManager.FunNeedValue;

        funNeedbar.current = value;
        funNeedNumber.text = value.ToString();
    }

    public override void OnPanelDeactivation()
    {
        base.OnPanelDeactivation();
    }
}
