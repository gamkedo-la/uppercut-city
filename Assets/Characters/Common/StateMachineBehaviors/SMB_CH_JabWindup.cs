using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SMB_CH_JabWindup : StateMachineBehaviour
{
    [SerializeField] [Range(6, 10)] float maxPower;
    [SerializeField] [Range(5, 20)] private float punchPowerupRate; // 'power' / second
    private CombatBehavior combatBehavior;
    private float punchPower;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetFloat("PunchPowerRight", 0);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // increment punch power
        punchPower += punchPowerupRate * Time.deltaTime;
        animator.SetFloat("PunchPowerRight", punchPower);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //animator.SetBool("JabWindup", false);
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
