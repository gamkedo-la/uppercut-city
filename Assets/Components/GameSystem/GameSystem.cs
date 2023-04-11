using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameSystem : MonoBehaviour
{
    public SO_GameSession gameSession;
    public TimeProvider timeProvider;
    public Animator masterStateMachine;
    public SO_GameType[] gameTypes;
    private int gameTypeIndex = 0;
    public SO_FighterControlData fighterAInput;
    public SO_FighterControlData fighterBInput;
    
    private void Awake()
    {
        // make new game session SO_GameSession for temp objects
        masterStateMachine = GetComponent<Animator>();
        gameSession.localInputs = FindObjectsOfType<PlayerInput>();
        MenuManager.acceptCharacters += HandleAcceptCharacters;
        PlayerController.newPlayerJoined += HandleNewPlayerJoined;
    }
    public void JoinNewPlayer(PlayerInput playerInput)
    {
        playerInput.uiInputModule = playerInput.GetComponent<InputSystemUIInputModule>();
        playerInput.uiInputModule.actionsAsset = playerInput.actions;
    }
    private void HandleNewPlayerJoined()
    {
        gameSession.localInputs = FindObjectsOfType<PlayerInput>();
    }
    public void ResetGameSession()
    {
        gameSession.currentRound = 1;
        gameSession.roundTime = gameTypes[gameTypeIndex].roundTime;
        gameSession.totalRounds = gameTypes[gameTypeIndex].numberOfRounds;
    }
    private void HandleAcceptCharacters(object sender, System.EventArgs e)
    {
        // later there will be another stage after
        ResetGameSession();
        masterStateMachine.SetBool("MatchStarted", true);
    }
    private void HandleStartMatch(object sender, System.EventArgs e)
    {
        ResetGameSession();
        masterStateMachine.SetBool("MatchStarted", true);
    }
}
