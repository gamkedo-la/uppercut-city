using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterStatus", menuName = "ScriptableObjects/FighterStatus", order = 1)]
public class SO_FighterStatus : ScriptableObject
{
    public static EventHandler onZeroHealth;
    public SO_FighterConfig fighterConfig;
    private float maxHealth = 100;
    private float maxStamina = 100;
    public float health;
    public float stamina;
    public float shortTermDamage;
    public void ResetFight()
    {
        health = maxHealth;
        stamina = maxStamina;
    }
    public void CheckStanding()
    {
        if (health <= 0)
        {
            onZeroHealth?.Invoke(this, EventArgs.Empty);
        }
    }
}
