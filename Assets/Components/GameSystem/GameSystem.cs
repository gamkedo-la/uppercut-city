using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameSystem : MonoBehaviour
{
    public SO_GameSession gameSession;
    public Animator masterStateMachine;
    public SO_GameType[] gameTypes;
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
    private void HandleAcceptCharacters(object sender, System.EventArgs e)
    {
        // later there will be another stage after
        masterStateMachine.SetBool("MatchStarted", true);
    }
    private void HandleStartMatch(object sender, System.EventArgs e)
    {
        masterStateMachine.SetBool("MatchStarted", true);
    }
}
