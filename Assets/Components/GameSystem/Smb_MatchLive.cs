using System;
using UnityEngine;
public class Smb_MatchLive : StateMachineBehaviour
{
    public delegate void MatchLiveUpdate();
    public static event MatchLiveUpdate onMatchLiveUpdate;
    public static EventHandler<EventArgs> onStateEnter;
    public static EventHandler onGamePaused;
    public static EventHandler onGameResume;
    private void PauseGame(object sender, EventArgs e)
    {
        Debug.Log("GamePaused");
        onGamePaused?.Invoke(this, EventArgs.Empty);
    }
    private void ResumeGame(object sender, EventArgs e)
    {
        Debug.Log("GameResumed");
        onGameResume?.Invoke(this, EventArgs.Empty);
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateEnter?.Invoke(this, EventArgs.Empty);
        PlayerInputHandling.onMenuPressed += PauseGame;
        UIInputHandling.onReturnPressed += ResumeGame;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onMatchLiveUpdate?.Invoke();
    }
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
