using UnityEngine;

public class FighterBehaviors : MonoBehaviour
{
    private Vector3 movementVector;
    public void SetMovementVector(Vector2 movementInput){
        movementVector.x = transform.position.x + movementInput.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + movementInput.y;
    }
    private void HandleMovement(){
        Debug.Log("moving character");
        transform.position = Vector3.MoveTowards(
            transform.position, 
            movementVector, 
            0.02f
        );
    }
    private void HandleRotation(){
        // rotate towards opponent
    }
    private void FixedUpdate() {
        if(movementVector.magnitude > 0.1f){
            HandleMovement();
        }
        HandleRotation();
    }
}
