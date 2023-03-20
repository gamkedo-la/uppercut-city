using System;
using System.Collections;
using UnityEngine;
public class SMB_CH_Followthrough : StateMachineBehaviour
{
    public delegate void FollowThrough();
    public enum PunchHand { right, left }
    public PunchHand punchHand;
    public AnimationCurve punchIKCurve;
    private CombatBehavior combatBehavior;
    private float curveResult;
    public static event FollowThrough onStateEnter;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateEnter?.Invoke();
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetBool("FollowThrough", true);
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        combatBehavior.EnablePunch(punchHand);

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        curveResult = punchIKCurve.Evaluate(stateInfo.normalizedTime);
        if(punchHand == PunchHand.right)
        {
            animator.SetFloat("IkRightWeight", curveResult);
        }
        if(punchHand == PunchHand.left)
        {
            animator.SetFloat("IkLeftWeight", curveResult);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("FollowThrough", false);
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetFloat("PunchPowerRight", 0);
        animator.SetFloat("PunchPowerLeft", 0);
        combatBehavior.DisablePunches();
    }
}
