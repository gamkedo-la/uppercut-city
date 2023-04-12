using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Gs_GameInSession : StateMachineBehaviour
{
    private GameSystem gameSystem;
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(!gameSystem){gameSystem = animator.GetComponent<GameSystem>();}
        gameSystem.ResetGameSession();
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
