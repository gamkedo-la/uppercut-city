using UnityEngine;
[CreateAssetMenu(fileName = "FighterConfig", menuName = "ScriptableObjects/FighterConfig", order = 1)]
public class SO_FighterConfig : ScriptableObject
{
    public delegate void FighterStatusEvent();
    public enum Corner {red, blue};
    [Header("Set Manually")]
    public Corner corner;
    public SO_Character activeCharacter;
    public SO_FighterConfig opponentConfig;
    public static float comboHoldTime = 3;
    public static float healthStart = 200;
    public static float staminaStart = 200;
    public static float burstDamageLimit = 35;
    public static float tempDamageLimit = 10;
    [Header("HUD Stats")]
    public float healthMax;
    public float healthCurrent;
    public float staminaMax;
    public float staminaCurrent;
    public int combo;
    public static event FighterStatusEvent onFighterDown;
    public event FighterStatusEvent onKnockedOut;
    public void SetNewMatch()
    {
        healthMax = healthStart;
        healthCurrent = healthStart;
        staminaMax = staminaStart;
        staminaCurrent = staminaStart;
        combo = 0;
    }
    public void CheckStatus()
    {
        if(healthCurrent <= 0)
        {
            onKnockedOut?.Invoke();
            onFighterDown?.Invoke();
            Debug.Log($"{corner} Knocked Out");
        }
    }
}
