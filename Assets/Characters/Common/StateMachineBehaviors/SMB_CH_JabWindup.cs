using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SMB_CH_JabWindup : StateMachineBehaviour
{
    [SerializeField] [Range(6, 10)] float maxPower;
    [SerializeField] [Range(5, 20)] private float punchPowerupRate; // 'power' / second
    private CombatBehavior combatBehavior;
    private float damage;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!combatBehavior){ combatBehavior = animator.GetComponent<CombatBehavior>(); }
        damage = combatBehavior.fighterConfig.activeCharacter.jabPower;
        animator.SetFloat("PunchPowerRight", damage);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        damage += Time.deltaTime;
        animator.SetFloat("PunchPowerRight", damage);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetFloat("PunchPowerRight", 0);
    }
}
