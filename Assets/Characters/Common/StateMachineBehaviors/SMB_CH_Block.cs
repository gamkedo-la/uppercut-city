using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SMB_CH_Followthrough;

public class SMB_CH_Block : StateMachineBehaviour
{
    public CombatBehavior combatBehavior;
    private float rAnalogAngle;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        
        // combatBehavior.EnablePunch(PunchHand.left);
        // combatBehavior.EnablePunch(PunchHand.right);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rAnalogAngle = animator.GetFloat("RStickAngle");
        if(animator.GetBool("FollowThrough")){return;}
        if(rAnalogAngle >= 0 && rAnalogAngle < 90)
        {
            // block top right
            combatBehavior.ActivateBlocker(0);
        }
        if(rAnalogAngle >= 90 && rAnalogAngle <= 180)
        {
            // block bottom right
            combatBehavior.ActivateBlocker(1);
        }
        if(rAnalogAngle < 0 && rAnalogAngle > -90)
        {
            // block top left
            combatBehavior.ActivateBlocker(2);
        }
        if(rAnalogAngle <= -90 && rAnalogAngle >= -180)
        {
            // block bottom left
            combatBehavior.ActivateBlocker(3);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // disable all blockers
        combatBehavior.DisablePunches();
        // using r-stick-angle switch block colliders on and off
    }
}
