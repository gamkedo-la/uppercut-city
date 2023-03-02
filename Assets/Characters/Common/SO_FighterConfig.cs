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
    public static float maxHealth = 200;
    public static float maxStamina = 200;
    public static float maxBurstDamage = 20;
    public float health;
    public float burstDamage;
    public float stamina;
    public void RestoreDefualts()
    {
        health = maxHealth;
        stamina = maxStamina;
        burstDamage = maxBurstDamage;
    }
    public void StatusCheck()
    {
        if (health <= 0)
        {
            // event 
        }
        if (burstDamage <= 0)
        {
            // event
        }
    }
}
