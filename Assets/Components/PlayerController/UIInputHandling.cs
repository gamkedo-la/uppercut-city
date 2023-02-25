using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class UIInputHandling : MonoBehaviour
{
    private PlayerController playerController;
    public static EventHandler<EventArgs> sideSelection;
    public static EventHandler onReturnPressed;
    private float sideSelectionAxis;
    private bool sideSelectionCooldown = false;
    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }
    private IEnumerator ChooseSides()
    {
        yield return new WaitForSeconds(.25f);
        sideSelectionCooldown = false;
    }
    public void HandleChooseSides(InputAction.CallbackContext context)
    {
        sideSelectionAxis = context.ReadValue<float>();
        Debug.Log($"Choose: {sideSelectionAxis}");
        if(sideSelectionAxis != 0 && !sideSelectionCooldown){
            sideSelectionCooldown = true;
            StartCoroutine(ChooseSides());
            // adjust allegiance flag on the player
            if(sideSelectionAxis > 0){
                playerController.playerConfig.IncrementAllegiance();
            } else {
                playerController.playerConfig.DecrementAllegiance();
            }
        }
        // cooldown selection, move once cooldown
        // Set the controller to 
        sideSelection?.Invoke(this, EventArgs.Empty);
    }
    public void HandleReturn(InputAction.CallbackContext context)
    {
        onReturnPressed?.Invoke(this, EventArgs.Empty);
    }
}
