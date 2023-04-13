using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameSystem : MonoBehaviour
{
    public delegate void GameSystemEvent();
    public SO_GameSession gameSession;
    public TimeProvider timeProvider;
    private Animator masterStateMachine;
    public SO_GameType gameType; // set a default, can be modified by menu
    
    private void Awake()
    {
        // make new game session SO_GameSession for temp objects
        masterStateMachine = GetComponent<Animator>();
        MenuManager.acceptCharacters += HandleAcceptCharacters;
    }
    public void JoinNewPlayer(PlayerInput playerInput)
    {
        playerInput.uiInputModule = playerInput.GetComponent<InputSystemUIInputModule>();
        playerInput.uiInputModule.actionsAsset = playerInput.actions;
    }
    public void ResetGameSession()
    {
        gameSession.currentRound = 0;
        gameSession.totalRounds = gameType.numberOfRounds;
        ResetRoundTime();
    }
    public void ResetRoundTime()
    {
        gameSession.roundTime = gameType.roundTime;
        gameSession.restTime = gameType.restTime;
    }
    private void HandleAcceptCharacters()
    {
        ResetGameSession();
        masterStateMachine.SetBool("GameInSession", true);
        masterStateMachine.SetBool("FightersToCorner", true);
    }
}
