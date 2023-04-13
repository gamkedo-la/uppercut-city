using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
public class HUDMessage : MonoBehaviour
{
    public SO_GameSession gameSession;
    public TextMeshProUGUI roundNumberText;
    public TextMeshProUGUI countDownText;
    private void Awake() 
    {
        StateFightersToCorners.onBetweenRoundUpdate += HandleBetweenRoundUpdate;
    }
    private void HandleBetweenRoundUpdate(float countDownTime)
    {
        roundNumberText.text = $"{gameSession.currentRound+1}";
        countDownText.text = $"{(int)Mathf.Clamp(countDownTime, 0, 99)}";
    }
}
