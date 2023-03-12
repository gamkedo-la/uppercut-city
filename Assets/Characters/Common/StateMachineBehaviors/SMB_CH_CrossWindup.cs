using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SMB_CH_CrossWindup : StateMachineBehaviour
{
    public CombatBehavior combatBehavior;
    private float damage;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        damage = combatBehavior.fighterConfig.activeCharacter.crossPower;
        animator.SetFloat("PunchPowerLeft", damage);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damage += Time.deltaTime;
        animator.SetFloat("PunchPowerLeft", damage);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetFloat("PunchPowerLeft", 0);
    }
}
