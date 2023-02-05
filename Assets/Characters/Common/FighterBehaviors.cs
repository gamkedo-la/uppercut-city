using UnityEngine;

public class FighterBehaviors : MonoBehaviour
{
    [SerializeField] BoxerConfig boxerConfig;
    private Animator animator;
    private GameObject opponent;
    private Vector3 movementVector;
    private bool moveStickActive = false;
    private void Awake() {
        // should initialize sides etc
        opponent = GameObject.FindWithTag("FighterB");
        // get animator
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("AnimationOffset", UnityEngine.Random.Range(0.1f, 10f));
    }
    public bool IsZeroQuaternion(Quaternion q){
        return q.x == 0 && q.y == 0 && q.z == 0 && q.w == 0;
    }
    public void SetLeanValues(Vector2 movementInput){
        
    }
    public void SetLeanModifier(bool leaning){
        animator.SetBool("Leaning", leaning);
    }
    public void SetMovementVector(Vector2 movementInput){
        animator.SetFloat("LStickX", movementInput.x);
        animator.SetFloat("LStickY", movementInput.y);
        movementVector.x = transform.position.x + movementInput.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + movementInput.y;
    }
    private void HandleGameStart(object sender, System.EventArgs e){
        animator.SetBool("FightStarted", true);
        // subscribe for round end event
    }
    public void HandlePunch(double inputAngle){
        // input angle: +- 180 left right  |  0 is neutral
        if (inputAngle == 0){
            // toggle the punch followthrough
            animator.SetTrigger("PunchFollowThrough");
            return;
        }
        if(inputAngle > 0 ){
            // right hand
            Debug.Log("right");
            if(!animator.GetBool("JabWindup")){ animator.SetBool("JabWindup", true); }
        } else {
            Debug.Log("left");
            if(!animator.GetBool("CrossWindup")){ animator.SetBool("CrossWindup", true); }
        }
        // see ticket https://trello.com/c/O1J6ZZxf
        // change animation state to windup
    }
    private void HandleMovement(){
        if(movementVector.magnitude <= 0.05f || animator.GetBool("Leaning")){ return; }
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
    private void Start() {
        StateGameStart.onStateEnter += HandleGameStart;
    }
    private void FixedUpdate() {
        HandleMovement();
        HandleRotation();
    }
}
