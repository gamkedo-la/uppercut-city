using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    private PlayerController playerController;
    public static event InputChooseSides onSideSelection;
    public static EventHandler onReturnPressed;
    private float sideSelectionAxis;
    public delegate void InputChooseSides(SO_PlayerConfig config, float sideSelectionAxis);
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void HandleChooseSides(InputAction.CallbackContext context)
    {
        sideSelectionAxis = context.ReadValue<float>();
        onSideSelection?.Invoke(
            playerController.playerConfig,
            sideSelectionAxis
        );
    }
    public void HandleReturn(InputAction.CallbackContext context)
    {
        Debug.Log("return pressed");
        onReturnPressed?.Invoke(this, EventArgs.Empty);
    }
}
