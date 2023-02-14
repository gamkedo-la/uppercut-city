using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    public static EventHandler<EventArgs> sideSelection;
    public void HandleNavigation(InputAction.CallbackContext context)
    {
        Debug.Log($"movement detected");
        // Set the controller to 
    }
    public void HandleChooseSides(InputAction.CallbackContext context)
    {
        Debug.Log($"Choose: {context.ReadValue<float>()}");
        // Set the controller to 
        sideSelection?.Invoke(this, EventArgs.Empty);
    }
}
