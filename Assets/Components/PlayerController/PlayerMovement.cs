using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector3 movementVector;
    private GameObject fighter;
    private Animator fighterAnimator;
    public string corner;
    private void Awake() {
        // Assign this player controller to a fighter
        // This is what the 'ChooseSides' menu scripting will do later on
        // ITMT: just grab player A
        fighter = GameObject.FindWithTag("FighterA");
    }
    public void InputMovement(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        movementVector.x = fighter.transform.position.x + movementInput.x;
        movementVector.y = fighter.transform.position.y;
        movementVector.z = fighter.transform.position.z + movementInput.y;
    }
    private void HandleMovement(){
        Debug.Log("moving character");
        fighter.transform.position = Vector3.MoveTowards(
            fighter.transform.position, 
            movementVector, 
            2f
        );
    }
    private void FixedUpdate() {
        if(movementInput.magnitude > 0.1f){
            HandleMovement();
        }
    }
}
