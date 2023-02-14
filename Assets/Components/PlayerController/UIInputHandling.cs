using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    public void HandleNavigation(InputAction.CallbackContext context)
    {
        Debug.Log($"movement detected");
        // Set the controller to 
    }
    public void HandleChooseSides(InputAction.CallbackContext context)
    {
        Debug.Log($"Choose: {context.ReadValue<float>()}");
        // Set the controller to 
    }
}
