using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public SO_FighterConfig fighterConfig;
    // text component for health
    public TextMeshProUGUI tmp_healthCurrent;
    public TextMeshProUGUI tmp_healthMax;
    public TextMeshProUGUI tmp_staminaText;
    public TextMeshProUGUI tmp_ComboText;
    // text component for stamina
    private void Awake() 
    {
        // on collision   
    }

    // map status values to UI elements
    private void FixedUpdate()
    {
        tmp_healthCurrent.text = ((int)fighterConfig.health_Current).ToString();
        tmp_healthMax.text = ((int)fighterConfig.health_Current).ToString();
        tmp_ComboText.text = fighterConfig.comboCurrent.ToString();
        tmp_staminaText.text = ((int)fighterConfig.stamina_Current).ToString();
    }
}
