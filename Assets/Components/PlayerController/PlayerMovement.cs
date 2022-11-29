using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector3 movementVector;
    private GameObject fighter;
    private Animator fighterAnimator;
    private void Awake() {
        fighter = GameObject.FindWithTag("FighterB");
        fighterAnimator = fighter.GetComponent<Animator>();
    }
    public void Movement(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        movementVector.x = movementInput.x;
        movementVector.y = fighter.transform.position.y;
        movementVector.z = movementInput.y; 
        fighter.transform.position = Vector3.MoveTowards(
            fighter.transform.position, 
            movementVector, 
            0.02f
        );
    }
    public void Punch(InputAction.CallbackContext context){
        Debug.Log("Punch");
    }
}
