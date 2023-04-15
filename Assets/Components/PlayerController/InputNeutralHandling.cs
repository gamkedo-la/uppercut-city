using UnityEngine;

public class InputNeutralHandling : MonoBehaviour
{
    // listen for skip
    public static event PlayerController.PlaterControllerEvent onRequestSkip;
    public void InputSkip(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            onRequestSkip?.Invoke();
        }
    }
}
