using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    private Vector3 movementVector;
    private GameObject fighter;
    private Animator fighterAnimator;
    private void Awake() {
        // things
    }
    public void InputMovement(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
        movementVector.x = transform.position.x + movementInput.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + movementInput.y;
    }
    private void HandleMovement(){
        Debug.Log("moving character");
        transform.position = Vector3.MoveTowards(
            transform.position, 
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
