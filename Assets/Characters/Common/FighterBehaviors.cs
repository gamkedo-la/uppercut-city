using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Animations.Rigging;
public class FighterBehaviors : MonoBehaviour
{
    public enum BlockType { right, left }
    public SO_FighterConfig fighterConfig;
    public TimeProvider timeProvider;
    public SO_FighterControlData inputData;
    public Animator animator;
    private FighterSetup fighterSetup;
    private FighterBehaviors opponentFighterBehaviors;
    private Vector3 movementVector;
    [Header("Punch Setup")]
    [SerializeField]public ChainIKConstraint rightArmIk;
    [SerializeField]public ChainIKConstraint leftArmIk;
    public GameObject[] handColliders;
    public GameObject head;
    public GameObject body;
    public GameObject punchTarget;
    public static EventHandler OnPunchThrown;
    private void Awake()
    {
        GetOpponentFighterBehaviors();
        StateGameStart.onStateEnter += HandleGameStart;
        // subscribe to events
    }
    public bool IsZeroQuaternion(Quaternion q)
    {
        return q.x == 0 && q.y == 0 && q.z == 0 && q.w == 0;
    }
    private void GetOpponentFighterBehaviors()
    {
        foreach (FighterBehaviors fb in FindObjectsOfType<FighterBehaviors>())
        {
            if(fb.fighterConfig.corner != fighterConfig.corner)
            {
                opponentFighterBehaviors = fb;
            }
        }
    }
    public void DisablePunches()
    {
        foreach (GameObject glove in handColliders)
        {
            glove.SetActive(false);
        }
    }
    public void EnablePunches()
    {
        foreach (GameObject glove in handColliders)
        {
            glove.SetActive(true);
        }
    }
    public void SetLeanModifier(bool leaning){
        animator.SetBool("Leaning", leaning);
    }
    public void SetMovementVector(Vector2 movementInput){
        movementVector.x = transform.position.x + movementInput.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + movementInput.y;
        if(fighterConfig.corner == SO_FighterConfig.Corner.blue)
        {
            animator.SetFloat("LStickX", movementInput.x*-1);
            animator.SetFloat("LStickY", movementInput.y*-1);
        }
        else
        {
            animator.SetFloat("LStickX", movementInput.x);
            animator.SetFloat("LStickY", movementInput.y);
        }
        // camera relative movement here
    }
    private void HandleGameStart()
    {
        animator.SetBool("FightStarted", true);
        GetOpponentFighterBehaviors();
        // subscribe for round end event
    }
    public void HandlePunch(double inputAngle)
    {
        // input angle: +180 right -180 left  |  0 is neutral
        animator.SetFloat("RStickAngle", (float)inputAngle);
    }
    private void HandleMovement()
    {
        if( movementVector.magnitude <= 0.05f || animator.GetBool("Leaning") ){ return; }
        transform.position = Vector3.MoveTowards(
            transform.position, 
            movementVector, 
            0.015f
        );
        // Todo: camera relative movement
    }
    private void HandleRotation()
    {
        if(opponentFighterBehaviors && (opponentFighterBehaviors.transform.position - transform.position) != Vector3.zero){
            // rotate towards opponent
            // TODO: limit the rate of rotation
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(opponentFighterBehaviors.transform.position - transform.position),
                timeProvider.fixedDeltaTime * 30f
            );
        }
    }

    public void HandleBlock(BlockType blocktype, bool v)
    {
        switch (blocktype)
        {
            case BlockType.right:
                animator.SetFloat("R1", v ? 1.0f : 0.0f);
                break;
            case BlockType.left:
                animator.SetFloat("RB", v ? 1.0f : 0.0f);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        rightArmIk.weight = animator.GetFloat("IkRightWeight");
        leftArmIk.weight = animator.GetFloat("IkLeftWeight");
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
}
