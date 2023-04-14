using System;
using UnityEngine;

public class Smb_Gs_BeginNewMatch : StateMachineBehaviour
{
    public delegate void GameStartEvent();
    public static event GameStartEvent onStateEnter;
    private GameSystem gameSystem;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Rematch");
        animator.SetBool("EndOfMatch", false);
        animator.SetBool("MatchLive", false);
        animator.SetBool("FightersToCorner", true);
        onStateEnter?.Invoke();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
