using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public SO_FighterConfig fighterConfig;
    public TextMeshProUGUI tmp_healthMax;
    public TextMeshProUGUI tmp_healthCurrent;
    public TextMeshProUGUI tmp_staminaMax;
    public TextMeshProUGUI tmp_staminaCurrent;
    public TextMeshProUGUI tmp_Combo;

    // map status values to UI elements
    private void FixedUpdate()
    {
        tmp_healthCurrent.text = ((int)fighterConfig.healthCurrent).ToString();
        tmp_healthMax.text = ((int)fighterConfig.healthMax).ToString();
        tmp_Combo.text = fighterConfig.combo.ToString();
        tmp_staminaMax.text = ((int)fighterConfig.staminaMax).ToString();
        tmp_staminaCurrent.text = ((int)fighterConfig.staminaCurrent).ToString();
    }
}
