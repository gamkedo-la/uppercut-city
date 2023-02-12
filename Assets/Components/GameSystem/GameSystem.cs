using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameSystem : MonoBehaviour
{
    public Animator masterStateMachine;
    public SO_GameType[] gameTypes;
    public SO_InputData fighterAInput;
    public SO_InputData fighterBInput;
    public static EventHandler<EventArgs> newPlayerJoined;
    private void Awake()
    {
        masterStateMachine = GetComponent<Animator>();
        MenuManager.acceptCharacters += HandleAcceptCharacters;
    }
    public void JoinNewPlayer(PlayerInput playerInput)
    {
        InputSystemUIInputModule UiModule = playerInput.GetComponent<InputSystemUIInputModule>();
        UiModule.actionsAsset = playerInput.actions;
        playerInput.uiInputModule = UiModule;
        // MenuManager.SetDefaultMenuFocus();
        newPlayerJoined?.Invoke(this, EventArgs.Empty);
        Debug.Log($"New Player: {playerInput.currentControlScheme}, {newPlayerJoined}");
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
