using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Gs_Knockdown : StateMachineBehaviour
{
    private GameSystem gameSystem;
    private float knockoutTime;
    public static event GameSystem.GameSystemEvent onStateEnter;
    public static event GameSystem.GameSystemEvent onStateExit;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!gameSystem){gameSystem = animator.GetComponent<GameSystem>();}
        animator.ResetTrigger("Knockdown");
        knockoutTime = gameSystem.timeProvider.time + 3;
        onStateEnter?.Invoke();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameSystem.gameSession.knockedDownCount = (int)(10 - Mathf.Clamp(gameSystem.timeProvider.time - knockoutTime, 0, 10) );
        if(gameSystem.timeProvider.time - knockoutTime >= 10)
        {
            animator.SetBool("EndOfMatch", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateExit?.Invoke();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
