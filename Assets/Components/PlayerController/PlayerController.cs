using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public SO_ControllerIconGroup inputIcons;
    public static event NewPlayerJoin newPlayerJoined;
    public SO_PlayerConfig playerConfig;
    public delegate void NewPlayerJoin();
    private void Awake()
    {
        playerConfig = ScriptableObject.CreateInstance<SO_PlayerConfig>();
        playerConfig.playerInput = GetComponent<PlayerInput>();
        playerConfig.playerInputHandling = GetComponent<PlayerInputHandling>();
        playerConfig.UiInputHandling = GetComponent<UIInputHandling>();
        switch (playerConfig.playerInput.currentControlScheme)
        {
            case "Keyboard&Mouse":
                playerConfig.controllerIcon = inputIcons.keyboardMouse;
                break;
            case "PS4":
                playerConfig.controllerIcon = inputIcons.playStation;
                break;
            case "XBox":
                playerConfig.controllerIcon = inputIcons.xBox;
                break;
            case "Gamepad":
                playerConfig.controllerIcon = inputIcons.genericGamepad;
                break;
        }
        Smb_MatchLive.onStateEnter += Ev_FightStart;
        Smb_MatchLive.onGamePaused += Ev_GamePaused;
        Smb_MatchLive.onGameResume += Ev_GameResumed;
        MenuManager.resumeGame += Ev_GameResumed;
        MenuManager.acceptCharacters += Ev_AcceptCharacter;
    }
    private void LiveMatchControls()
    {
        if(playerConfig.allegiance == SO_PlayerConfig.Allegiance.neutral)
        {
            playerConfig.playerInput.SwitchCurrentActionMap("Neutral");
        } else {
            playerConfig.playerInput.SwitchCurrentActionMap("Player");
        }
    }
    private void MenuControls()
    {
        if(playerConfig.allegiance == SO_PlayerConfig.Allegiance.neutral)
        {
            playerConfig.playerInput.SwitchCurrentActionMap("Neutral");
        } else {
            playerConfig.playerInput.SwitchCurrentActionMap("UI");
        }
    }
    public void Ev_FightStart()
    {
        // get allegiance
        // set the fighterInput
        LiveMatchControls();
    }
    public void Ev_AcceptCharacter()
    {
        // map the controller to the fighter
        playerConfig.playerInputHandling.LinkToFighter();
    }
    public void Ev_GamePaused()
    {
        MenuControls();
    }
    public void Ev_GameResumed()
    {
        LiveMatchControls();
    }
    private void Start()
    {
        switch (FindObjectsOfType<PlayerInput>().Length)
        {
            case 1:
                playerConfig.allegiance = SO_PlayerConfig.Allegiance.red;
                break;
            case 2:
                playerConfig.allegiance = SO_PlayerConfig.Allegiance.blue;
                break;
            default:
                playerConfig.allegiance = SO_PlayerConfig.Allegiance.neutral;
                break;
        }
        Debug.Log($"New Player: {playerConfig.playerInput.currentControlScheme}");
        newPlayerJoined?.Invoke();
    }
}
