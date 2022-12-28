using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector3 movementVector;
    private GameObject fighter;
    private Animator fighterAnimator;
    private void Awake() {
        fighter = GameObject.FindWithTag("FighterA");
        // fighterAnimator = fighter.GetComponent<Animator>();
    }
    public void InputMovement(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        if(movementInput.magnitude > 0.1f){
            // update state machine to moving
        }
        
    }
    public void Punch(InputAction.CallbackContext context){
        Debug.Log("Punch");
    }
    private void FixedUpdate() {
        
    }
}
