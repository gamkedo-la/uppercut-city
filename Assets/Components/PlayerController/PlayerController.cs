using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public delegate void PlaterControllerEvent();
    public SO_ControllerIconGroup inputIcons;
    public static event PlaterControllerEvent newPlayerJoined;
    public SO_PlayerConfig playerConfig;
    private PlayerController[] inputsAll;
    private bool redIsOpen;
    private bool blueIsOpen;
    private void Awake()
    {
        playerConfig = ScriptableObject.CreateInstance<SO_PlayerConfig>();
        playerConfig.playerInput = GetComponent<PlayerInput>();
        playerConfig.playerInputHandling = GetComponent<PlayerInputHandling>();
        playerConfig.UiInputHandling = GetComponent<UIInputHandling>();
        playerConfig.allegiance = SO_PlayerConfig.Allegiance.neutral;
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
        StateGameSetup.onStateEnter += MenuControls;
        Smb_MatchLive.onStateEnter += LiveMatchControls;
        Smb_MatchLive.onGamePaused += MenuControls;
        Smb_MatchLive.onGameResume += LiveMatchControls;
        Smb_Gs_EndOfRound.onStateEnter += NeutralControls;
        Smb_Gs_EndOfMatch.onStateMachineEnter += MenuControls;
        StateFightersToCorners.onStateEnter += NeutralControls;
        MenuManager.gameSessionEnd += MenuControls;
        MenuManager.resumeGame += LiveMatchControls;
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
    private void NeutralControls()
    {
        playerConfig.playerInput.SwitchCurrentActionMap("Neutral");
    }
    private void Start()
    {
        inputsAll = FindObjectsOfType<PlayerController>();
        redIsOpen = true;
        blueIsOpen = true;
        foreach (PlayerController player in inputsAll)
        {
            if(player == this){break;}
            if(player.playerConfig.allegiance == SO_PlayerConfig.Allegiance.red) {redIsOpen = false;}
            if(player.playerConfig.allegiance == SO_PlayerConfig.Allegiance.blue) {blueIsOpen = false;}
        }
        if(blueIsOpen)
        {
            playerConfig.allegiance = SO_PlayerConfig.Allegiance.blue;
        }
        if(redIsOpen)
        {
            playerConfig.allegiance = SO_PlayerConfig.Allegiance.red;
        }
        Debug.Log($"New Player: {playerConfig.playerInput.currentControlScheme}");
        newPlayerJoined?.Invoke();
    }
}
