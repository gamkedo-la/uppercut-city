using UnityEngine;

public class FighterBehaviors : MonoBehaviour
{
    [SerializeField] BoxerConfig boxerConfig;
    private GameObject opponent;
    private Vector3 movementVector;
    private bool moveStickActive = false;
    private void Awake() {
        // should initialize sides etc
        opponent = GameObject.FindWithTag("FighterB");
    }
    public bool IsZeroQuaternion(Quaternion q){
        return q.x == 0 && q.y == 0 && q.z == 0 && q.w == 0;
    }
    public void SetMovementVector(Vector2 movementInput){
        movementVector.x = transform.position.x + movementInput.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + movementInput.y;
    }
    public void HandlePunch(double inputAngle){
        if(inputAngle > 0 ){
            // right hand
            Debug.Log("right hand punch");
        } else {
            Debug.Log("left hand punch");
        }
        // see ticket https://trello.com/c/O1J6ZZxf
        // change animation state to windup
    }
    private void HandleMovement(){
        transform.position = Vector3.MoveTowards(
            transform.position, 
            movementVector, 
            0.02f
        );
        // Todo: camera relative movement
    }
    private void HandleRotation(){
        // rotate towards opponent
        // TODO: limit the rate of rotation
        if((opponent.transform.position - transform.position) != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(opponent.transform.position - transform.position);;
        }
    }
    private void FixedUpdate() {
        HandleMovement();
        HandleRotation();
    }
}
