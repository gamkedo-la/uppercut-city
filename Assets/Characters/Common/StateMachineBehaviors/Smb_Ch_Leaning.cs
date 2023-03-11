using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Ch_Leaning : StateMachineBehaviour
{
   private CombatBehavior combatBehavior;
   public delegate void OnLeaningUpdate(SO_FighterConfig.Corner corner, float lStickX);
   public static event OnLeaningUpdate onLeaningUpdate;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      onLeaningUpdate?.Invoke(combatBehavior.fighterConfig.corner, animator.GetFloat("LStickX"));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //animator.SetLayerWeight(1,0);
    }

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
