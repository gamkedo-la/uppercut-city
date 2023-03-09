using System;
using System.Collections;
using UnityEngine;
public class SMB_CH_Followthrough : StateMachineBehaviour
{
    public enum PunchHand { right, left }
    public AnimationCurve punchIKCurve;
    public PunchHand punchHand;
    private Animator thisAnimator;
    private FighterBehaviors fighterBehaviors;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        fighterBehaviors = animator.GetComponentInParent<FighterBehaviors>();
        // TODO: how much stamina should be used?
        // Punch power should be taken into consideration
        animator.SetFloat("StaminaCurrent", animator.GetFloat("StaminaCurrent") - 10);
        fighterBehaviors.EnablePunches();
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
        animator.SetBool("JabWindup", false);
        animator.SetBool("CrossWindup", false);
        animator.ResetTrigger("PunchFollowThrough");
        fighterBehaviors.DisablePunches();
    }
}
