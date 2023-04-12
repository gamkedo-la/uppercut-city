using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    // reference to the game session
    public SO_GameSession gameSession;
    public TextMeshProUGUI tmpRoundNumber;
    public TextMeshProUGUI tmpTotalRounds;
    public TextMeshProUGUI tmpRoundTime;
    private int minutes;
    private string seconds;
    private void Awake()
    {
        Smb_MatchLive.onMatchLiveUpdate += MatchLiveUpdate;
        Smb_MatchLive.onStateEnter += UpdateRounds;
        StateFightersToCorners.onStateEnter += UpdateRounds;    
    }
    private void MatchLiveUpdate()
    {
        minutes = (int)(gameSession.roundTime/60);
        seconds = gameSession.roundTime % 60 < 10 ?
            $"0{(int)gameSession.roundTime % 60}" :
            $"{(int)gameSession.roundTime % 60}";
        tmpRoundTime.text = $"{minutes}:{seconds}";
    }
    private void UpdateRounds()
    {
        tmpRoundNumber.text = gameSession.currentRound.ToString();
        tmpTotalRounds.text = gameSession.totalRounds.ToString();
    }
}
