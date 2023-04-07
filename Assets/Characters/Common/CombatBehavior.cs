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
    [HideInInspector] public float punchCooldown;
    private float punchCooldownTimer;
    private float hitTimer;
    private float punchThrownTimer;
    [HideInInspector] public PunchTarget punchTarget = PunchTarget.head;
    [Header("IK Components")]
    public ChainIKConstraint rightArmHeadIk;
    public ChainIKConstraint leftArmHeadIk;
    public ChainIKConstraint rightArmBodyIk;
    public ChainIKConstraint leftArmBodyIk;
    public PunchTargetBehavior headTarget;
    public PunchTargetBehavior bodyTarget;
    [Header("Hit Detection")]
    public GameObject[] blockers; // has to be 0 - TR, 1 - BR, 2 - TL, 3 - BL
    public Transform bodyTransform;
    public Transform headTransform;
    public PunchDetector hitBodyDetector;
    public PunchDetector hitHeadDetector;
    [Header("Punches")]
    public GameObject rightHand;
    public GameObject leftHand;
    [HideInInspector] public Collider rightHandCollider;
    [HideInInspector] public Collider leftHandCollider;
    [HideInInspector] public AttackProperties leftAttackProperties;
    [HideInInspector] public AttackProperties rightAttackProperties;
    [Header("VFX Components")]
    public GameObject leftWristFireEmitter;
    public GameObject rightWristFireEmitter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rightHandCollider = rightHand.GetComponent<Collider>();
        rightAttackProperties = rightHand.GetComponent<AttackProperties>();
        leftHandCollider = leftHand.GetComponent<Collider>();
        leftAttackProperties = leftHand.GetComponent<AttackProperties>();
        hitBodyDetector.onHitReceived += BodyHitReceived;
        hitHeadDetector.onHitReceived += HeadHitReceived;
        leftAttackProperties.onHitOpponent += SuccessfulPunch;
        rightAttackProperties.onHitOpponent += SuccessfulPunch;
        StateGameStart.onStateEnter += HandleGameStart;
        Smb_MatchLive.onMatchLiveUpdate += MatchLiveUpdate;
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
        headTarget.enabled = false;
        bodyTarget.enabled = false;
        if(punchHand == SMB_CH_Followthrough.PunchHand.right)
        {
            rightHand.SetActive(true);
            rightAttackProperties.gameObject.SetActive(true);
            rightAttackProperties.punchDamage = animator.GetFloat("PunchPowerRight");
            fighterConfig.staminaCurrent -= rightAttackProperties.punchDamage;
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
            onRightPunchThrown?.Invoke(this, EventArgs.Empty);
            if(animator.GetFloat("PunchPowerRight") >= 10)
            {
                rightWristFireEmitter.SetActive(true);
            }
            return;
        }
        if(punchHand == SMB_CH_Followthrough.PunchHand.left)
        {
            leftHand.SetActive(true);
            leftAttackProperties.gameObject.SetActive(true);
            leftAttackProperties.punchDamage = animator.GetFloat("PunchPowerLeft");
            fighterConfig.staminaCurrent -= leftAttackProperties.punchDamage;
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
            onLeftPunchThrown?.Invoke(this, EventArgs.Empty);
            // powerful punches make a fire trail
            if(animator.GetFloat("PunchPowerLeft") >= 10)
            {
                leftWristFireEmitter.SetActive(true);
            }
            return;
        }
    }
    public void ActivateBlocker(int index)
    {
        for(int i=0 ; i<blockers.Length ; i++)
        {
            if(i == index)
            {
                blockers[i].SetActive(true);
            }
            else
            {
                blockers[i].SetActive(false);
            }
        }

    }
    public void BodyHitReceived(float damage)
    {
        Debug.Log($"Body: {damage}");
        fighterConfig.healthCurrent -= damage;
        fighterConfig.staminaCurrent -= damage / 1.2f;
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
            fighterConfig.healthMax -= damage / 5;
        }
        fighterConfig.healthCurrent -= damage;
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        hitTimer = fighterConfig.activeCharacter.healCooldown;
    }
    private IEnumerator PunchImpact()
    {
        while(timeProvider.time - punchCooldownTimer <= punchCooldown)
        {
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("PunchLanded", true);
        animator.speed = 1;
        yield break;
    }
    public void SuccessfulPunch(float damage)
    {
        Debug.Log($"Hit opponent for: {damage}");
        PunchFinished();
        punchCooldown = damage / 20;
        punchCooldownTimer = timeProvider.time;
        // slow-mo sort of
        animator.speed = 0.01f;
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        StartCoroutine(PunchImpact());
    }
    public void PunchFinished()
    {
        rightAttackProperties.gameObject.SetActive(false);
        leftAttackProperties.gameObject.SetActive(false);
        leftWristFireEmitter.SetActive(false);
        rightWristFireEmitter.SetActive(false);
        // Targets Can Move again
        headTarget.enabled = true;
        bodyTarget.enabled = true;
        animator.SetBool("WindUp", false);
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
    public void GuardUpdate()
    {
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
                fighterConfig.staminaCurrent + fighterConfig.activeCharacter.staminaRegenRate*timeProvider.fixedDeltaTime,
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
