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
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetBool("FollowThrough", true);
        // if we're leaning forward
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        // punch the body if we're leaning forward, otherwise always head
        if( animator.GetBool("Leaning") && animator.GetFloat("LStickX") > 0.5)
        {
            combatBehavior.punchTarget = CombatBehavior.PunchTarget.body;
            Debug.Log("Target the body");
        }
        else
        {
            combatBehavior.punchTarget = CombatBehavior.PunchTarget.head;
            Debug.Log("Target the Head");
        }
        // check punch power and add VFX if it's high enough
        if(punchHand == PunchHand.left && animator.GetFloat("PunchPowerLeft") >= 5)
        {
            combatBehavior.leftWristFireEmitter.SetActive(true);
        }
        combatBehavior.EnablePunch(punchHand);
        onStateEnter?.Invoke();
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
        animator.SetBool("WindUp", false);
        animator.SetFloat("IkRightWeight", 0);
        animator.SetFloat("IkLeftWeight", 0);
        animator.SetFloat("PunchPowerRight", 0);
        animator.SetFloat("PunchPowerLeft", 0);
        combatBehavior.DisablePunches();
    }
}
