using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Smb_Ch_Neutral : StateMachineBehaviour
{
    private CombatBehavior combatBehavior;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
    }
}
