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
    public static float health_Start = 200;
    public static float stamina_Start = 200;
    public static float BurstDamage_Start = 35;
    public float health_Max;
    public float burstDamage_Max;
    public float stamina_Max;
    public float health_Current;
    public float burstDamage_Current;
    public float stamina_Current;
    public void RestoreDefualts()
    {
        health_Max = health_Start;
        stamina_Max = stamina_Start;
        burstDamage_Max = BurstDamage_Start;
        health_Current = health_Start;
        stamina_Current = stamina_Start;
        burstDamage_Current = BurstDamage_Start;
    }
    public void StatusCheck()
    {
        if (health_Current <= 0)
        {
            // event 
        }
        if (burstDamage_Current <= 0)
        {
            // event
        }
    }
}
