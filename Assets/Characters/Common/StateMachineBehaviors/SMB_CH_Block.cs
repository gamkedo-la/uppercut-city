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
        combatBehavior.blockLeftArm.gameObject.SetActive(true);
        combatBehavior.blockRightArm.gameObject.SetActive(true);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rAnalogAngle = animator.GetFloat("RStickAngle");
        Debug.Log($"rAnalogAngle: {rAnalogAngle}");
        if(animator.GetBool("FollowThrough")){return;}
        if(rAnalogAngle >= 0 && rAnalogAngle < 90)
        {
            // block top right
            if(combatBehavior.headHitRight.gameObject.activeSelf)
            {
                combatBehavior.EnableAllPunchDetectors();
                combatBehavior.headHitRight.gameObject.SetActive(false);
                return;
            }
            
        }
        if(rAnalogAngle >= 90 && rAnalogAngle <= 180)
        {
            // block bottom right
            if(combatBehavior.bodyHitRight.gameObject.activeSelf)
            {
                combatBehavior.EnableAllPunchDetectors();
                combatBehavior.bodyHitRight.gameObject.SetActive(false);
                return;
            }
        }
        if(rAnalogAngle < 0 && rAnalogAngle > -90)
        {
            // block top left
            if(combatBehavior.headHitLeft.gameObject.activeSelf)
            {
                combatBehavior.EnableAllPunchDetectors();
                combatBehavior.headHitLeft.gameObject.SetActive(false);
                return;
            }
        }
        if(rAnalogAngle <= -90 && rAnalogAngle >= -180)
        {
            // block bottom left
            if(combatBehavior.bodyHitLeft.gameObject.activeSelf == false)
            {
                combatBehavior.EnableAllPunchDetectors();
                combatBehavior.bodyHitLeft.gameObject.SetActive(false);
                return;
            }
            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatBehavior.blockLeftArm.gameObject.SetActive(false);
        combatBehavior.blockRightArm.gameObject.SetActive(false);
        combatBehavior.EnableAllPunchDetectors();
    }
}
