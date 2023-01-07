using UnityEngine;

public class FighterBehaviors : MonoBehaviour
{
    [SerializeField] BoxerConfig boxerConfig;
    private GameObject opponent;
    private Vector3 movementVector;
    private void Awake() {
        // should initialize sides etc
        opponent = GameObject.FindWithTag("FighterB");
    }
    public void SetMovementVector(Vector2 movementInput){
        movementVector.x = transform.position.x + movementInput.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + movementInput.y;
    }
    private void HandleMovement(){
        Debug.Log("moving character");
        // Todo: camera relative movement
        transform.position = Vector3.MoveTowards(
            transform.position, 
            movementVector, 
            0.02f
        );
    }
    private void HandleRotation(){
        // rotate towards opponent
        // TODO: limit the rate of rotation
        transform.rotation = Quaternion.LookRotation(opponent.transform.position - transform.position);
    }
    private void FixedUpdate() {
        if(movementVector.magnitude > 0.1f){
            HandleMovement();
        }
        HandleRotation();
    }
}
