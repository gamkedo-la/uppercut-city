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
    private Transform fightCamTransform;
    private FighterSetup fighterSetup;
    private FighterBehaviors opponentFighterBehaviors;
    private Vector3 movementVector;
    private Vector3 cameraForwardVector;
    private Vector3 cameraRightVector;
    [Header("Punch Setup")]
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
    public void SetBlockModifier(bool blocking){
        animator.SetBool("Blocking", blocking);
    }
    public void SetMovementVector(Vector2 movementInput)
    {
        if(movementInput.magnitude <= 0.1f){
            movementVector = Vector3.zero;
            return;
        }
        //Debug.Log($"");
        cameraForwardVector = movementInput.y * Camera.main.transform.forward;
        cameraRightVector = movementInput.x * Camera.main.transform.right;
        movementVector = new Vector3(
            (cameraForwardVector.x + cameraRightVector.x), 
            transform.position.y, 
            (cameraForwardVector.z + cameraRightVector.z)
        );
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
    }
    private void HandleGameStart()
    {
        animator.SetBool("FightStarted", true);
        GetOpponentFighterBehaviors();
        // subscribe for round end event
    }
    public void HandlePunch(Vector2 punchInput)
    {
        // input angle: +180 right -180 left  |  0 is neutral
        // Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg
        animator.SetFloat("RStickAngle", Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg);
        animator.SetFloat("RStickX", punchInput.x);
        animator.SetFloat("RStickY", punchInput.y);
    }
    private void HandleMovement()
    {
        if( movementVector.magnitude <= 0.05f || animator.GetBool("Leaning") || animator.GetBool("FollowThrough") ){ return; }
        transform.position = Vector3.MoveTowards(
            transform.position, 
            movementVector, 
            0.015f
        );
    }
    private void HandleRotation()
    {
        if(opponentFighterBehaviors && (opponentFighterBehaviors.transform.position - transform.position) != Vector3.zero){
            // rotate towards opponent
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(opponentFighterBehaviors.transform.position - transform.position),
                timeProvider.fixedDeltaTime * 45f
            );
        }
    }
    public void Blocking(float button) {
    {
        animator.SetBool("Blocking", button > 0);
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
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
}
