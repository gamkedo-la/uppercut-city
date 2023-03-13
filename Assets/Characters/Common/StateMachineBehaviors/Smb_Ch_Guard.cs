using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Ch_Guard : StateMachineBehaviour
{
    private CombatBehavior combatBehavior;
    public delegate void OnGuardUpdate(SO_FighterConfig.Corner corner);
    public static event OnGuardUpdate onGuardUpdate;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetFloat("PunchPowerLeft", 0);
        animator.SetFloat("PunchPowerRight", 0);
        combatBehavior.DisablePunches();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onGuardUpdate?.Invoke(combatBehavior.fighterConfig.corner);
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
