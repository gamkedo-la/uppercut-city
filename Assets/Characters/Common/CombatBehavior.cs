using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CombatBehavior : MonoBehaviour
{
    public enum PunchTarget { head, body }
    public SO_FighterConfig fighterConfig;
    public GameObject rightHand;
    public GameObject leftHand;
    //damage dealer class
    private Collider rightHandCollider;
    private Collider leftHandCollider;
    [HideInInspector] public AttackProperties leftAttackProperties;
    [HideInInspector] public AttackProperties rightAttackProperties;
    public PunchDetector hitBodyDetector;
    public PunchDetector hitHeadDetector;
    private Animator animator;
    private PunchTarget punchTarget = PunchTarget.head;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightHandCollider = rightHand.GetComponent<Collider>();
        rightAttackProperties = rightHand.GetComponent<AttackProperties>();
        leftHandCollider = leftHand.GetComponent<Collider>();
        leftAttackProperties = leftHand.GetComponent<AttackProperties>();
        StateGameStart.onStateEnter += HandleGameStart;
        Smb_MatchLive.onMatchLiveUpdate += MatchLiveUpdate;
        Smb_Ch_Leaning.onLeaningUpdate += LeaningUpdate;
    }
    private void HandleGameStart()
    {
        DisablePunches();
        fighterConfig.SetNewMatch();
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        animator.SetFloat("HealthMax", fighterConfig.healthMax);
        animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
        animator.SetFloat("StaminaMax", fighterConfig.staminaMax);
        animator.SetFloat("Combo", fighterConfig.combo);

    }
    public void DisablePunches()
    {
        rightHand.SetActive(false);
        leftHand.SetActive(false);
        rightAttackProperties.punchDamage = 0;
        leftAttackProperties.punchDamage = 0;
    }
    public void EnablePunch(SMB_CH_Followthrough.PunchHand punchHand)
    {
        if(punchHand == SMB_CH_Followthrough.PunchHand.right)
        {
            rightHand.SetActive(true);
            rightAttackProperties.punchDamage = animator.GetFloat("PunchPowerRight");
            animator.SetFloat("StaminaCurrent", animator.GetFloat("StaminaCurrent") - rightAttackProperties.punchDamage);
            return;
        }
        if(punchHand == SMB_CH_Followthrough.PunchHand.left)
        {
            leftHand.SetActive(true);
            leftAttackProperties.punchDamage = animator.GetFloat("PunchPowerLeft");
            animator.SetFloat("StaminaCurrent", animator.GetFloat("StaminaCurrent") - leftAttackProperties.punchDamage);
            return;
        }
    }
    private void LeaningUpdate(SO_FighterConfig.Corner evCorner, float lStickX)
    {
        if(fighterConfig.corner != evCorner){ return; }
        if(lStickX > 0  && punchTarget != PunchTarget.body)
        {
            punchTarget = PunchTarget.body;
            // set the ik target component to the body
            Debug.Log("Target the body");
        }
        if(lStickX <= 0 && punchTarget != PunchTarget.head)
        {
            punchTarget = PunchTarget.head;
            // set the ik target component to the head
            Debug.Log("Target the Head");
        }
    }
    public void PunchFinished()
    {
        animator.SetBool("FollowThrough", false);
    }
    private void HandleHealthRegen()
    {
        // When we take a hit, health regen is temporarily disabled
    }
    private void MatchLiveUpdate()
    {
        fighterConfig.healthCurrent = animator.GetFloat("HealthCurrent");
        fighterConfig.healthMax = animator.GetFloat("HealthMax");
        fighterConfig.staminaCurrent = animator.GetFloat("StaminaCurrent");
        fighterConfig.staminaMax = animator.GetFloat("StaminaMax");
        fighterConfig.combo = (int)animator.GetFloat("Combo");
    }
}
