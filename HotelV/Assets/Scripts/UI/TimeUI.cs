using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        UpdateTick();
    }
    [SerializeField]
    private TMP_Text tickNumber;
    private void UpdateTick()
    {
        tickNumber.text = TickManager.Instance.TickCounter.ToString();
    }
}
