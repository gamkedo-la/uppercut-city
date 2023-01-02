using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandling : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 movementInput;
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
        if(movementInput.magnitude > 0.1f){
            fighterBehaviors.SetMovementVector(movementInput);
        } else{
            fighterBehaviors.SetMovementVector(Vector2.zero);
        }
    }
}
