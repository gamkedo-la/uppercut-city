using System.Collections;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class CombatBehavior : MonoBehaviour
{
    public delegate void CombatEvent();
    public event CombatEvent onRightPunchThrown;
    public event CombatEvent onLeftPunchThrown;

    public enum PunchTarget { head, body }
    [Header("Systems / Behavior")]
    public TimeProvider timeProvider;
    public SO_FighterConfig fighterConfig;
    public FighterBehaviors fighterBehaviors;
    private Animator animator;
    [HideInInspector] public float punchCooldown;
    private float punchCooldownTimer;
    [HideInInspector] public float stunDuration;
    private float stunTimer;
    private float hitTimer;
    private float punchThrownTimer;
    public PunchTarget punchTarget;
    [Header("IK Components")]
    public ChainIKConstraint rightArmHeadIk;
    public ChainIKConstraint leftArmHeadIk;
    public ChainIKConstraint rightArmBodyIk;
    public ChainIKConstraint leftArmBodyIk;
    public PunchTargetBehavior headTarget;
    public PunchTargetBehavior bodyTarget;
    [Header("Hit Detection")]
    public BlockCollider blockLeftArm;
    public BlockCollider blockRightArm;
    public PunchDetector bodyHitLeft;
    public PunchDetector bodyHitRight;
    public PunchDetector headHitLeft;
    public PunchDetector headHitRight;
    [Header("Punches")]
    public GameObject rightHand;
    public GameObject leftHand;
    [HideInInspector] public Collider rightHandCollider;
    [HideInInspector] public Collider leftHandCollider;
    [HideInInspector] public AttackProperties leftAttackProperties;
    [HideInInspector] public AttackProperties rightAttackProperties;
    private float timeLastHit;
    [Header("VFX Components")]
    public TrailRenderer rightTrailSpeedLines;
    public TrailRenderer leftTrailSpeedLines;
    public FireTrail leftFireEmitter;
    public FireTrail rightFireEmitter;
    
    [Header("Facial Expression Material Swap")]
    public bool facialExpressionsEnabled = true;
    public SkinnedMeshRenderer changeMyMaterial;
    public Material faceMaterial;
    public Material blinkMaterial;
    public Material grimaceMaterial;
    public int faceMaterialSlot = 0; // FIXME: hardcoded!! this must be the "HEAD" material
    public float blinkTimespan = 0.15f; // very short timespan
    public float blinkMinDelay = 1.0f;
    public float blinkMaxDelay = 3.0f;
    public float grimaceTimespan = 2.0f;
    private bool blinking = false;
    private float blinkDelay = 1f;
    private float grimaceTimeLeft = 0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        fighterConfig.onKnockedOut += KnockedOut;
        rightHandCollider = rightHand.GetComponent<Collider>();
        rightAttackProperties = rightHand.GetComponent<AttackProperties>();
        leftHandCollider = leftHand.GetComponent<Collider>();
        leftAttackProperties = leftHand.GetComponent<AttackProperties>();
        bodyHitLeft.onHitReceived += BodyHitReceived;
        bodyHitRight.onHitReceived += BodyHitReceived;
        headHitLeft.onHitReceived += HeadHitReceived;
        headHitRight.onHitReceived += HeadHitReceived;
        blockLeftArm.onBlockedPunch += BlockedPunch;
        blockRightArm.onBlockedPunch += BlockedPunch;
        leftAttackProperties.onHitOpponent += SuccessfulPunch;
        rightAttackProperties.onHitOpponent += SuccessfulPunch;
        leftAttackProperties.onGotBlocked += GotBlocked;
        rightAttackProperties.onGotBlocked += GotBlocked;
        Smb_Gs_BeginNewMatch.onStateEnter += HandleGameStart;
        Smb_MatchLive.onMatchLiveUpdate += MatchLiveUpdate;
    }
    private void HandleGameStart()
    {
        DisablePunches();
        animator.enabled = true;
        animator.speed = 1;
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
    public void EnableAllPunchDetectors()
    {
        bodyHitLeft.gameObject.SetActive(true);
        bodyHitRight.gameObject.SetActive(true);
        headHitLeft.gameObject.SetActive(true);
        headHitRight.gameObject.SetActive(true);
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
            if(rightAttackProperties.punchDamage >= SO_FighterConfig.tempDamageLimit)
            {
                rightFireEmitter.EmitParticles();
            }
            fighterConfig.staminaCurrent -= rightAttackProperties.punchDamage / 2;
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
            onRightPunchThrown?.Invoke();
            return;
        }
        if(punchHand == SMB_CH_Followthrough.PunchHand.left)
        {
            leftHand.SetActive(true);
            leftAttackProperties.gameObject.SetActive(true);
            leftAttackProperties.punchDamage = animator.GetFloat("PunchPowerLeft");
            if(leftAttackProperties.punchDamage >= SO_FighterConfig.tempDamageLimit)
            {
                leftFireEmitter.EmitParticles();
            }
            fighterConfig.staminaCurrent -= leftAttackProperties.punchDamage / 2;
            animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
            onLeftPunchThrown?.Invoke();
            return;
        }
    }
    public void BodyHitReceived(float damage)
    {
        if(damage >= SO_FighterConfig.tempDamageLimit)
        {
            // rules for taking a power punch
            fighterConfig.staminaMax -= damage / 3;
            fighterConfig.healthCurrent -= damage / 3;
            fighterConfig.staminaCurrent -= damage / 3;
        }
        else
        {
            fighterConfig.healthCurrent -= damage / 2;
            fighterConfig.staminaCurrent -= damage / 2;
        }
        
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        animator.SetFloat("StaminaCurrent", fighterConfig.staminaCurrent);
        fighterConfig.CheckStatus();
        hitTimer = fighterConfig.activeCharacter.healCooldown;
    }
    public void HeadHitReceived(float damage)
    {
        if(damage >= SO_FighterConfig.tempDamageLimit)
        {
            // rules for taking a power punch
            fighterConfig.healthMax -= damage * 0.2f;
            fighterConfig.healthCurrent -= damage * 0.8f;
        }
        else
        {
            fighterConfig.healthCurrent -= damage;
        }
        animator.SetFloat("HealthCurrent", fighterConfig.healthCurrent);
        fighterConfig.CheckStatus();
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
        if(timeProvider.time - timeLastHit < 0.2f){ return;} // multiple hits
        PunchFinished();

        // increment the combo count
        fighterConfig.combo++;
        timeLastHit = timeProvider.time;
        animator.SetFloat("Combo", fighterConfig.combo);

        punchCooldown = damage / 20;
        punchCooldownTimer = timeProvider.time;
        // slow-mo sort of
        animator.speed = 0.01f;
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        StartCoroutine(PunchImpact());
    }
    private IEnumerator Stunned()
    {
        while(timeProvider.time - stunTimer <= stunDuration)
        {
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Stunned", false);
        yield break;
    }
    public void GotBlocked(float power)
    {
        // reset the combo count
        fighterConfig.combo = 0;
        timeLastHit = timeProvider.time;
        animator.SetFloat("Combo", fighterConfig.combo);

        PunchFinished();
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetBool("Stunned", true);
        stunDuration = power / 8; // how long it lasts
        stunTimer = timeProvider.time;
        StartCoroutine(Stunned());
        // duration is based on on punch power
    }
    public void BlockedPunch()
    {
        // blockage
        animator.SetTrigger("SuccessfulBlock");
    }
    public void PunchFinished()
    {
        rightAttackProperties.gameObject.SetActive(false);
        leftAttackProperties.gameObject.SetActive(false);
        leftFireEmitter.StopEmission();
        rightFireEmitter.StopEmission();
        rightTrailSpeedLines.emitting = false;
        leftTrailSpeedLines.emitting = false;
        headTarget.enabled = true;
        bodyTarget.enabled = true;
        animator.SetBool("WindUp", false);
    }
    private void KnockedOut()
    {
        animator.enabled = false;
        fighterConfig.healthMax -= fighterConfig.healthMax * 0.15f;
        fighterConfig.staminaMax -= fighterConfig.staminaMax * 0.15f;
        // knockdown sequence, re center model later
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
    private void HandleCombo()
    {
        if(fighterConfig.combo > 0)
        {
            if(timeProvider.time - timeLastHit > SO_FighterConfig.comboHoldTime)
            {
                fighterConfig.combo = 0;
                animator.SetFloat("Combo", fighterConfig.combo);
            }
        }
    }
    public void GuardUpdate()
    {
        if(punchThrownTimer > 0)
        {
            punchThrownTimer = Mathf.Clamp(
                punchThrownTimer - timeProvider.deltaTime,
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
    
    private void maybeBlinkEyes() {
        blinkDelay -= timeProvider.fixedDeltaTime;
        if (blinkDelay <= 0f) {        
            if (blinking) {

                Debug.Log(gameObject.name + "'s eyes open");
                blinkDelay = UnityEngine.Random.Range(blinkMinDelay,blinkMaxDelay);
                blinking = false;

                if (changeMyMaterial && changeMyMaterial.materials.Length >= faceMaterialSlot) {
                    Debug.Log("previous face material "+faceMaterialSlot+": "+changeMyMaterial.materials[faceMaterialSlot].name);
                    
                    // this should work but doesn't
                    // changeMyMaterial.materials[faceMaterialSlot] = faceMaterial;

                    // FIXME: it appears that changing the material is ignored and has no effect? =(
                    // this is a unity bug, but here's the hack solution workaround to force unity to notice:
                    Material[] mats = changeMyMaterial.materials;
                    mats[faceMaterialSlot] = faceMaterial;
                    changeMyMaterial.materials = mats;

                    Debug.Log("current face material "+faceMaterialSlot+": "+changeMyMaterial.materials[faceMaterialSlot].name);
                    Debug.Log("it should be: "+faceMaterial.name); 
                } else {
                    Debug.Log("missing head material slot " + faceMaterialSlot);
                }

            } else {

                Debug.Log(gameObject.name + "'s eyes close");
                blinkDelay = blinkTimespan;
                blinking = true;

                if (changeMyMaterial && changeMyMaterial.materials.Length >= faceMaterialSlot) {
                    Debug.Log("previous face material "+faceMaterialSlot+": "+changeMyMaterial.materials[faceMaterialSlot].name);

                    // this should work but doesn't
                    // changeMyMaterial.materials[faceMaterialSlot] = blinkMaterial;
                    
                    // FIXME: it appears that changing the material is ignored and has no effect? =(
                    // this is a unity bug, but here's the hack solution workaround to force unity to notice:
                    Material[] mats = changeMyMaterial.materials;
                    mats[faceMaterialSlot] = blinkMaterial;
                    changeMyMaterial.materials = mats;

                    Debug.Log("current face material "+faceMaterialSlot+": "+changeMyMaterial.materials[faceMaterialSlot].name);
                    Debug.Log("it should be: "+blinkMaterial.name);

                } else {
                    Debug.Log("missing head material slot " + faceMaterialSlot);
                }

            }
        }
    }
    
    private void Update()
    {
        // handle punch ik
        HandleIk();

        HandleCombo();

        // randomly blink eyes
        if (facialExpressionsEnabled) maybeBlinkEyes();
    }
}
