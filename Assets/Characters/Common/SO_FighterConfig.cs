using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(fileName = "FighterConfig", menuName = "ScriptableObjects/FighterConfig", order = 1)]
public class SO_FighterConfig : ScriptableObject
{
    public enum Corner {red, blue};
    [Header("Set Manually")]
    public Corner corner;
    public SO_Character activeCharacter;
    public SO_FighterConfig opponentConfig;
    public static float healthStart = 200;
    public static float staminaStart = 200;
    public static float burstDamageLimit = 35;
    [Header("HUD Stats")]
    public float healthMax;
    public float healthCurrent;
    public float staminaMax;
    public float staminaCurrent;
    public int combo;

}
