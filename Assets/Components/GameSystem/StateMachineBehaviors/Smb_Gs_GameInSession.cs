using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Gs_GameInSession : StateMachineBehaviour
{
    private GameSystem.GameSystemEvent onStateMachineEnter;
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        onStateMachineEnter?.Invoke();
    }
    // override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    // {
    // }
}
