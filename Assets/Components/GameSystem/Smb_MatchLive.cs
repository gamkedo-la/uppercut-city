using System;
using UnityEngine;
public class Smb_MatchLive : StateMachineBehaviour
{
    public static EventHandler<EventArgs> onStateEnter;
    public static EventHandler onGamePaused;
    private void PauseGame(object sender, EventArgs e)
    {
        Debug.Log("GamePaused");
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateEnter?.Invoke(this, EventArgs.Empty);
        PlayerInputHandling.onMenuPressed += PauseGame;
    }
    
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       PlayerInputHandling.onMenuPressed -= PauseGame;
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
