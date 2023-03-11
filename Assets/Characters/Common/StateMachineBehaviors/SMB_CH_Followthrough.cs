using System;
using System.Collections;
using UnityEngine;
public class SMB_CH_Followthrough : StateMachineBehaviour
{
    public enum PunchHand { right, left }
    public PunchHand punchHand;
    public AnimationCurve punchIKCurve;
    private CombatBehavior combatBehavior;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetBool("FollowThrough", true);
        // TODO: how much stamina should be used?
        // Punch power should be taken into consideration
        animator.SetFloat("StaminaCurrent", animator.GetFloat("StaminaCurrent") - 10);
        combatBehavior.EnablePunch(punchHand);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(punchHand == PunchHand.right)
        {
            animator.SetFloat("IkRightWeight", punchIKCurve.Evaluate(stateInfo.normalizedTime));
        }
        if(punchHand == PunchHand.left)
        {
            animator.SetFloat("IkLeftWeight", punchIKCurve.Evaluate(stateInfo.normalizedTime));
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetBool("FollowThrough", false);
        animator.SetBool("JabWindup", false);
        animator.SetBool("CrossWindup", false);
        animator.ResetTrigger("PunchFollowThrough");
        if(punchHand == PunchHand.right)
        {
            animator.SetFloat("PunchPowerRight", 0);
        }
        if(punchHand == PunchHand.left)
        {
            animator.SetFloat("PunchPowerLeft", 0);
        }
        combatBehavior.DisablePunches();
    }
}
