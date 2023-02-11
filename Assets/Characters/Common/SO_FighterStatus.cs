using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterStatus", menuName = "ScriptableObjects/FighterStatus", order = 1)]
public class SO_FighterStatus : ScriptableObject
{
    public SO_FighterConfig fighterConfig;
    public float health;
    public float stamina;
    public float shortTermDamage;
}
