using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandling : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 movementInput;
    private Vector2 punchInput;
    private GameObject fighter;
    private FighterBehaviors fighterBehaviors;
    private Animator fighterAnimator;
    public string corner;
    // connect this object to a fighter
    // input logic goes in here
    // behaviours are handled in FighterBehaviors
    private void Awake(){
        mainCamera = Camera.main;
        // Assign this player controller to a fighter
        // This is what the 'ChooseSides' menu scripting will do later on
        // ITMT: just grab player A
        fighter = GameObject.FindWithTag("FighterA");
        fighterBehaviors = fighter.GetComponent<FighterBehaviors>();
    }
    public void InputMovement(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        fighterBehaviors.SetMovementVector(movementInput);
    }
    public void HandlePunchInput(InputAction.CallbackContext context){
        punchInput = context.ReadValue<Vector2>();
        fighterBehaviors.HandlePunch(Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg);
    }
}
