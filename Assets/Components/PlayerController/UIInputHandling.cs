using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    public static EventHandler<EventArgs> sideSelection;
    public void HandleChooseSides(InputAction.CallbackContext context)
    {
        Debug.Log($"Choose: {context.ReadValue<float>()}");
        // Set the controller to 
        sideSelection?.Invoke(this, EventArgs.Empty);
    }
}
