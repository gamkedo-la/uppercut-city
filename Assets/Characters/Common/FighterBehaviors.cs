using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Animations.Rigging;
public class FighterBehaviors : MonoBehaviour
{
    public delegate void FighterStateEvent();
    public enum BlockType { right, left }
    public SO_FighterConfig fighterConfig;
    public Transform corner;
    public Transform stareDownTransform;
    public TimeProvider timeProvider;
    public Animator animator;
    private FighterSetup fighterSetup;
    private FighterBehaviors opponentFighterBehaviors;
    private Vector3 movementVector;
    private Vector3 cameraForwardVector;
    private Vector3 cameraRightVector;
    private void Awake()
    {
        FindMyCorner();
        GetOpponentFighterBehaviors();
        StateGameSetup.onStateEnter += MoveToShowdown;
        Smb_Gs_MoveToCenter.onStateEnter += MoveToShowdown;
        Smb_Gs_BeginNewMatch.onStateEnter += HandleGameStart;
        StateFightersToCorners.onStateEnter += MoveToCorner;
        Smb_MatchLive.onStateEnter += EnterCombat;
    }
    public bool IsZeroQuaternion(Quaternion q)
    {
        return q.x == 0 && q.y == 0 && q.z == 0 && q.w == 0;
    }
    private void FindMyCorner()
    {
        if(fighterConfig.corner == SO_FighterConfig.Corner.red)
        {
            corner = GameObject.FindWithTag("RedCorner").transform;
            stareDownTransform = GameObject.FindWithTag("RedStareDown").transform;
        }
        if(fighterConfig.corner == SO_FighterConfig.Corner.blue)
        {
            corner = GameObject.FindWithTag("BlueCorner").transform;
            stareDownTransform = GameObject.FindWithTag("BlueStareDown").transform;
        }
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
    private void EnterCombat()
    {
        animator.SetBool("InCombat", true);
    }
    private void OutOfCombat()
    {
        animator.SetBool("InCombat", false);
    }
    public void SetLeanModifier(bool leaning){
        animator.SetBool("Leaning", leaning);
    }
    public void SetBlockModifier(bool blocking){
        animator.SetBool("Blocking", blocking);
    }
    public void SetMovementVector(Vector2 movementInput)
    {
        if(movementInput.magnitude <= 0.2f){
            movementVector = Vector3.zero;
            return;
        }
        cameraForwardVector = Camera.main.transform.forward*movementInput.y + Camera.main.transform.right*movementInput.x;
        movementVector.x = transform.position.x + cameraForwardVector.x;
        movementVector.y = transform.position.y;
        movementVector.z = transform.position.z + cameraForwardVector.z;
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
        GetOpponentFighterBehaviors();
    }
    public void HandlePunch(Vector2 punchInput)
    {
        // input angle: +180 right -180 left  |  0 is neutral
        // Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg
        animator.SetFloat("RStickAngle", Mathf.Atan2(punchInput.x, punchInput.y) * Mathf.Rad2Deg);
        animator.SetFloat("RStickX", punchInput.x);
        animator.SetFloat("RStickY", punchInput.y);
    }
    public void MoveToCorner()
    {
        if(!corner){FindMyCorner();}
        OutOfCombat();
        transform.position = new Vector3(
            corner.position.x,
            transform.position.y,
            corner.position.z
        );
    }
    public void MoveToShowdown()
    {
        if(!stareDownTransform){FindMyCorner();}
        OutOfCombat();
        transform.position = new Vector3(
            stareDownTransform.position.x,
            transform.position.y,
            stareDownTransform.position.z
        );

    }
    public void HandleMovement()
    {
        // BUGFIX: player is able to move during menus and other times they shouldn't
        if( movementVector.magnitude <= 0.05f || animator.GetBool("Leaning") || animator.GetBool("FollowThrough") ){ return; }
        transform.position = Vector3.MoveTowards(
            transform.position, 
            movementVector, 
            fighterConfig.activeCharacter.movementSpeed * timeProvider.fixedDeltaTime
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
    public void Blocking(float button) 
    {
        animator.SetBool("Blocking", button > 0);
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
