using System;
using UnityEngine;

public class StateGameSetup : StateMachineBehaviour
{
    public static GameSystem.GameSystemEvent onStateEnter;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       onStateEnter?.Invoke();
    }
    
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
