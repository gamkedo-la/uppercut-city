using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public SO_FighterStatus fighterStatus;
    // text component for health
    public TextMeshProUGUI tmp_healthText;
    public TextMeshProUGUI tmp_staminaText;
    // text component for stamina
    private void Awake() 
    {
        // on collision   
    }

    // map status values to UI elements
    private void FixedUpdate()
    {
        tmp_healthText.text = ((int)fighterStatus.health).ToString();
        tmp_staminaText.text = ((int)fighterStatus.stamina).ToString();
    }
}
