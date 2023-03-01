using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    private PlayerController playerController;
    public static EventHandler<InputChooseSides> onSideSelection;
    public static EventHandler onReturnPressed;
    private float sideSelectionAxis;
    public class InputChooseSides : EventArgs
    {
        public SO_PlayerConfig config;
        public float sideSelectionAxis;
    }
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    
    public void HandleChooseSides(InputAction.CallbackContext context)
    {
        sideSelectionAxis = context.ReadValue<float>();
        onSideSelection?.Invoke(this, new InputChooseSides{
            config = playerController.playerConfig,
            sideSelectionAxis = sideSelectionAxis
        });
    }
    public void HandleReturn(InputAction.CallbackContext context)
    {
        Debug.Log("return pressed");
        onReturnPressed?.Invoke(this, EventArgs.Empty);
    }
}
