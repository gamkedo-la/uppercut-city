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
    private float stamina;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        staminaRegenDelayTime = 0;
        animator.ResetTrigger("PunchFollowThrough");
        fighterBehaviors = animator.GetComponentInParent<FighterBehaviors>();
        fighterBehaviors.DisablePunches();
        staminaRegenDelayTime = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"Animator {timeProvider}"); //BUG: code that references SO_TimeProvider should be the same for all instances but isn't.
        stamina = Mathf.Clamp(
            animator.GetFloat("StaminaCurrent") + staminaRegenRate*.005f,
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
