using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SMB_CH_Followthrough;

public class SMB_CH_Block : StateMachineBehaviour
{
    public CombatBehavior combatBehavior;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!combatBehavior) { combatBehavior = animator.GetComponent<CombatBehavior>(); }
        
        combatBehavior.EnablePunch(PunchHand.left);
        combatBehavior.EnablePunch(PunchHand.right);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatBehavior.DisablePunches();
    }
}
