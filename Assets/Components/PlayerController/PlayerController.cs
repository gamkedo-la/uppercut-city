using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public SO_ControllerIconGroup inputIcons;
    public static EventHandler<EventArgs> newPlayerJoined;
    public SO_PlayerConfig playerConfig;
    private void Awake()
    {
        Debug.Log($"PlayerController Awake");
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
    }
    public void Ev_FightStart(object sender, System.EventArgs e)
    {
        // get allegiance
        // set the fighterInput
        if(playerConfig.allegiance == SO_PlayerConfig.Allegiance.neutral)
        {
            playerConfig.playerInput.SwitchCurrentActionMap("Neutral");
        } else {
            playerConfig.playerInput.SwitchCurrentActionMap("Player");
        }
    }
    private void Start() {
        PlayerInput[] localInputs = FindObjectsOfType<PlayerInput>();
        switch (localInputs.Length)
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
        newPlayerJoined?.Invoke(this, EventArgs.Empty);
    }
}
