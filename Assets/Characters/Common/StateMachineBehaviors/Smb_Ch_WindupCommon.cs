using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Smb_Ch_WindupCommon : StateMachineBehaviour
{
    [HideInInspector] public CombatBehavior combatBehavior;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        animator.SetBool("WindUp", true);
    }
}
