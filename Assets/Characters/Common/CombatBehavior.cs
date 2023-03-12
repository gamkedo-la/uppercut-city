using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CombatBehavior : MonoBehaviour
{
    public enum PunchTarget { head, body }
    public TimeProvider timeProvider;
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
    private float hitTimer;
    private PunchTarget punchTarget = PunchTarget.head;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightHandCollider = rightHand.GetComponent<Collider>();
        rightAttackProperties = rightHand.GetComponent<AttackProperties>();
        leftHandCollider = leftHand.GetComponent<Collider>();
        leftAttackProperties = leftHand.GetComponent<AttackProperties>();
        hitBodyDetector.onHitReceived += BodyHitReceived;
        hitHeadDetector.onHitReceived += HeadHitReceived;
        StateGameStart.onStateEnter += HandleGameStart;
        Smb_MatchLive.onMatchLiveUpdate += MatchLiveUpdate;
        Smb_Ch_Leaning.onLeaningUpdate += LeaningUpdate;
        Smb_Ch_Guard.onGuardUpdate += GuardUpdate;
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
    public void BodyHitReceived(float damage)
    {
        Debug.Log($"Body: {damage}");
        fighterConfig.healthCurrent -= damage;
        fighterConfig.staminaCurrent -= damage / 2;
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        animator.SetFloat("StaminaCurrent", fighterConfig.healthCurrent);
        hitTimer = fighterConfig.activeCharacter.healCooldown;
    }
    public void HeadHitReceived(float damage)
    {
        Debug.Log($"Head: {damage}");
        if(damage > 10){
            // damages max health
            fighterConfig.healthMax -= damage / 10;
            animator.SetFloat("HealthMax", fighterConfig.healthMax);
        }
        fighterConfig.healthCurrent -= damage;
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        hitTimer = fighterConfig.activeCharacter.healCooldown;
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
        if(hitTimer > 0)
        {
            hitTimer = Mathf.Clamp(
                hitTimer - timeProvider.fixedDeltaTime, 
                0,
                fighterConfig.activeCharacter.healCooldown
            );
        }
        else
        {
            fighterConfig.healthCurrent = Mathf.Clamp(
                fighterConfig.healthCurrent + fighterConfig.activeCharacter.healthRegenRate * timeProvider.fixedDeltaTime, 
                0,
                fighterConfig.healthMax
            );
            animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        }
        
    }
    private void GuardUpdate(SO_FighterConfig.Corner evCorner)
    {
        // when in the guard we can regen stamina
        if(fighterConfig.corner != evCorner){ return; }
        fighterConfig.staminaCurrent = Mathf.Clamp(
            fighterConfig.staminaCurrent + fighterConfig.activeCharacter.staminaRegenRate*timeProvider.deltaTime,
            0,
            fighterConfig.staminaMax
        );
        animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
    }
    private void MatchLiveUpdate()
    {
        HandleHealthRegen();
        fighterConfig.staminaCurrent = animator.GetFloat("StaminaCurrent");
        fighterConfig.combo = (int)animator.GetFloat("Combo");
    }
}
