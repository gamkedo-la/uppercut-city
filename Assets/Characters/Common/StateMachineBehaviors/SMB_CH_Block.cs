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
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rAnalogAngle = animator.GetFloat("RStickAngle");
        if(animator.GetBool("FollowThrough")){return;}
        if(rAnalogAngle >= 0 && rAnalogAngle < 90)
        {
            // block top right
            if(combatBehavior.blockTopRight.gameObject.activeSelf == false)
            {
                combatBehavior.DisableBlockers();
                combatBehavior.blockTopRight.gameObject.SetActive(true);
                return;
            }
            
        }
        if(rAnalogAngle >= 90 && rAnalogAngle <= 180)
        {
            // block bottom right
            if(combatBehavior.blockBottomRight.gameObject.activeSelf == false)
            {
                combatBehavior.DisableBlockers();
                combatBehavior.blockBottomRight.gameObject.SetActive(true);
                return;
            }
        }
        if(rAnalogAngle < 0 && rAnalogAngle > -90)
        {
            // block top left
            if(combatBehavior.blockTopLeft.gameObject.activeSelf == false)
            {
                combatBehavior.DisableBlockers();
                combatBehavior.blockTopLeft.gameObject.SetActive(true);
                return;
            }
        }
        if(rAnalogAngle <= -90 && rAnalogAngle >= -180)
        {
            // block bottom left
            if(combatBehavior.blockBottomLeft.gameObject.activeSelf == false)
            {
                combatBehavior.DisableBlockers();
                combatBehavior.blockBottomLeft.gameObject.SetActive(true);
                return;
            }
            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // disable all blockers
        combatBehavior.DisableBlockers();
        // using r-stick-angle switch block colliders on and off
    }
}
