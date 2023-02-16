using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public SO_ControllerIconGroup inputIcons;
    public static EventHandler<EventArgs> newPlayerJoined;
    public SO_PlayerConfig playerConfig;
    private PlayerInput playerInput;
    private void Awake()
    {
        
        playerConfig = ScriptableObject.CreateInstance<SO_PlayerConfig>();
        playerInput = GetComponent<PlayerInput>();
        switch (playerInput.currentControlScheme)
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
        Debug.Log($"");
        Smb_MatchLive.onStateEnter += Ev_FightStart;
    }
    public void Ev_FightStart(object sender, System.EventArgs e){
        // get allegiance
        // set the fighterInput
        if(playerConfig.allegiance == SO_PlayerConfig.Allegiance.neutral)
        {
            playerInput.SwitchCurrentActionMap("Neutral");
        } else {
            playerInput.SwitchCurrentActionMap("Player");
        }
    }
    private void Start() {
        Debug.Log($"New Player: {playerInput.currentControlScheme}");
        newPlayerJoined?.Invoke(this, EventArgs.Empty);
    }
}
