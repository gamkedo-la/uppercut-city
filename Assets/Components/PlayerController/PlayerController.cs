using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public delegate void PlaterControllerEvent();
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
        StateGameSetup.onStateEnter += MenuControls;
        Smb_MatchLive.onStateEnter += LiveMatchControls;
        Smb_MatchLive.onGamePaused += MenuControls;
        Smb_MatchLive.onGameResume += LiveMatchControls;
        Smb_MatchLive.onStateExit += NeutralControls;
        Smb_Gs_EndOfMatch.onStateMachineEnter += MenuControls;
        StateFightersToCorners.onStateEnter += NeutralControls;
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
