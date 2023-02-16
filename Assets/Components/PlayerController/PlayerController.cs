using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] MultiplayerEventSystem mpEventSystem;
    [SerializeField] InputSystemUIInputModule mpUiInputModule;
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
        Debug.Log($"New Player: {playerInput.currentControlScheme}");
        newPlayerJoined?.Invoke(this, EventArgs.Empty);
    }
}
