using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Ch_Guard : StateMachineBehaviour
{
    [SerializeField] [Range(0, 5f)] float staminaRegenRate;
    [SerializeField] [Range(0, 5f)] float healthRegenRate;
    [SerializeField] [Range(0, 5f)] float staminaRegenDelay; // 1 or 2 breaths before stamina starts going up again
    [SerializeField] TimeProvider timeProvider;
    private float staminaRegenDelayTime;
    private FighterBehaviors fighterBehaviors;
    private CombatBehavior combatBehavior;
    private float stamina;
    public delegate void OnGuardUpdate(SO_FighterConfig.Corner corner);
    public static event OnGuardUpdate onGuardUpdate;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetFloat("PunchPowerLeft", 0);
        animator.SetFloat("PunchPowerRight", 0);
        staminaRegenDelayTime = 0;
        combatBehavior.DisablePunches();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onGuardUpdate?.Invoke(combatBehavior.fighterConfig.corner);
        stamina = Mathf.Clamp(
            animator.GetFloat("StaminaCurrent") + staminaRegenRate*Time.deltaTime,
            0,
            animator.GetFloat("StaminaMax")
        );
        animator.SetFloat("StaminaCurrent", stamina);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
