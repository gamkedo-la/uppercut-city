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
        // activate the punch colliders
        fighterBehaviors = animator.GetComponentInParent<FighterBehaviors>();
        fighterBehaviors.EnablePunches();
        //fighterBehaviors.rightArmIk.SetIkWeight(1);
        // subscribe to event
        // is it right hand or left hand?
        // punch type?
        // set Target transform
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ease in the punch IK
        if(punchHand == PunchHand.right)
        {
            // ease in right hand IK
            fighterBehaviors.SetRightArmIkWeight(punchIKCurve.Evaluate(stateInfo.normalizedTime));
        }
        if(punchHand == PunchHand.left)
        {
            // ease in left hand IK
            //fighterBehaviors.SetLeftArmIkWeight(punchIKCurve.Evaluate(stateInfo.normalizedTime));
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("JabWindup", false);
        animator.SetBool("CrossWindup", false);
        // deactivate punch colliders
        fighterBehaviors.DisablePunches();
        if(punchHand == PunchHand.right)
        {
            fighterBehaviors.SetRightArmIkWeight(0f);
        }
        if(punchHand == PunchHand.left)
        {
            // zero left arm ik
            //fighterBehaviors.SetLeftArmIkWeight(punchIKCurve.Evaluate(stateInfo.normalizedTime));
        }
    }
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that sets up animation IK (inverse kinematics)
    }
}
