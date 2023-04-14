using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public SO_FighterConfig fighterConfig;
    public TextMeshProUGUI tmp_Combo;
    public Slider healthMaxBar;
    public Slider healthBar;
    public Slider staminaMaxBar;
    public Slider staminaBar;
    private void Awake()
    {
        StateFightersToCorners.onStateEnter += UpdateHUD;
        Smb_MatchLive.onMatchLiveUpdate += UpdateHUD;
    }
    private void UpdateHUD()
    {
        healthMaxBar.value = fighterConfig.healthMax / SO_FighterConfig.healthStart;
        healthBar.value = fighterConfig.healthCurrent / SO_FighterConfig.healthStart;
        staminaMaxBar.value = fighterConfig.staminaMax / SO_FighterConfig.staminaStart;
        staminaBar.value = fighterConfig.staminaCurrent / SO_FighterConfig.staminaStart;
        tmp_Combo.text = fighterConfig.combo.ToString();
    }
}
