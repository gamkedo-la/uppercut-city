using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;
using System;

public class CombatBehavior : MonoBehaviour
{

    public event EventHandler onRightPunchThrown;
    public event EventHandler onLeftPunchThrown;

    public enum PunchTarget { head, body }
    [Header("Systems / Behavior")]
    public TimeProvider timeProvider;
    public SO_FighterConfig fighterConfig;
    private Animator animator;
    private float hitTimer;
    private float punchThrownTimer;
    [HideInInspector] public PunchTarget punchTarget = PunchTarget.head;
    private bool targetSwitched = false;
    [Header("IK Components")]
    public ChainIKConstraint rightArmHeadIk;
    public ChainIKConstraint leftArmHeadIk;
    public ChainIKConstraint rightArmBodyIk;
    public ChainIKConstraint leftArmBodyIk;
    public PunchTargetBehavior headTarget;
    public PunchTargetBehavior bodyTarget;
    [Header("Combat Components")]
    public Transform bodyTransform;
    public Transform headTransform;
    public PunchDetector hitBodyDetector;
    public PunchDetector hitHeadDetector;
    public PunchDetector hitRightGloveDetector;
    public PunchDetector hitLeftGloveDetector;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject rightHandBlock;
    public GameObject leftHandBlock;
    [HideInInspector] public Collider rightHandCollider;
    [HideInInspector] public Collider leftHandCollider;
    
    [HideInInspector] public AttackProperties leftAttackProperties;
    [HideInInspector] public AttackProperties rightAttackProperties;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightHandCollider = rightHand.GetComponent<Collider>();
        rightAttackProperties = rightHand.GetComponent<AttackProperties>();
        leftHandCollider = leftHand.GetComponent<Collider>();
        leftAttackProperties = leftHand.GetComponent<AttackProperties>();
        hitBodyDetector.onHitReceived += BodyHitReceived;
        hitHeadDetector.onHitReceived += HeadHitReceived;
        hitRightGloveDetector.onHitReceived += HitGloveReceived;
        hitLeftGloveDetector.onHitReceived += HitGloveReceived;
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
        punchThrownTimer = fighterConfig.activeCharacter.staminaCooldown;
    }
    public void EnablePunch(SMB_CH_Followthrough.PunchHand punchHand)
    {
        if(punchHand == SMB_CH_Followthrough.PunchHand.right)
        {
            rightHand.SetActive(true);
            rightAttackProperties.punchDamage = animator.GetFloat("PunchPowerRight");
            fighterConfig.staminaCurrent -= rightAttackProperties.punchDamage;
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
            onRightPunchThrown?.Invoke(this, EventArgs.Empty);
            return;
        }
        if(punchHand == SMB_CH_Followthrough.PunchHand.left)
        {
            leftHand.SetActive(true);
            leftAttackProperties.punchDamage = animator.GetFloat("PunchPowerLeft");
            fighterConfig.staminaCurrent -= leftAttackProperties.punchDamage;
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
            onLeftPunchThrown?.Invoke(this, EventArgs.Empty);
            return;
        }
    }
    public void BodyHitReceived(float damage)
    {
        Debug.Log($"Body: {damage}");
        fighterConfig.healthCurrent -= damage;
        fighterConfig.staminaCurrent -= damage / 2;
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
        hitTimer = fighterConfig.activeCharacter.healCooldown;
    }
    public void HeadHitReceived(float damage)
    {
        Debug.Log($"Head: {damage}");
        if(damage > 10)
        {
            // damages max health
            fighterConfig.healthMax -= damage / 10;
        }
        fighterConfig.healthCurrent -= damage;
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        hitTimer = fighterConfig.activeCharacter.healCooldown;
    }
    public void HitGloveReceived(float damage) //Aka block
    {
        Debug.Log($"Blocked");
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
    private void HandleIk()
    {
        if(punchTarget == PunchTarget.head)
        {
            rightArmBodyIk.weight = 0;
            leftArmBodyIk.weight = 0;
            leftArmHeadIk.weight = animator.GetFloat("IkLeftWeight");
            rightArmHeadIk.weight = animator.GetFloat("IkRightWeight");
        }
        if(punchTarget == PunchTarget.body)
        {
            rightArmBodyIk.weight = animator.GetFloat("IkRightWeight");
            leftArmBodyIk.weight = animator.GetFloat("IkLeftWeight");
            leftArmHeadIk.weight = 0;
            rightArmHeadIk.weight = 0;
        }
    }
    private void LeaningUpdate(SO_FighterConfig.Corner evCorner, float lStickX)
    {
        if(fighterConfig.corner != evCorner){ return; }
        if(lStickX > 0.5  && punchTarget != PunchTarget.body)
        {
            punchTarget = PunchTarget.body;
            Debug.Log("Target the body");
        }
        if(lStickX <= 0.5 && punchTarget != PunchTarget.head)
        {
            punchTarget = PunchTarget.head;
            Debug.Log("Target the Head");
        }
    }
    private void GuardUpdate(SO_FighterConfig.Corner evCorner)
    {
        // when in the guard we can regen stamina
        if(fighterConfig.corner != evCorner){ return; }
        if(punchThrownTimer > 0)
        {
            punchThrownTimer = Mathf.Clamp(
                punchThrownTimer - timeProvider.fixedDeltaTime,
                0,
                fighterConfig.activeCharacter.staminaCooldown
            );
        }
        else
        {
            fighterConfig.staminaCurrent = Mathf.Clamp(
                fighterConfig.staminaCurrent + fighterConfig.activeCharacter.staminaRegenRate*timeProvider.deltaTime,
                0,
                fighterConfig.staminaMax
            );
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
        }
    }
    private void MatchLiveUpdate()
    {
        HandleHealthRegen();
        fighterConfig.combo = (int)animator.GetFloat("Combo");
    }
    private void Update()
    {
        // handle punch ik
        HandleIk();       
    }
}
