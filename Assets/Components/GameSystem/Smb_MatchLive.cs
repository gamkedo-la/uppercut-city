using System;
using UnityEngine;
public class Smb_MatchLive : StateMachineBehaviour
{
    public delegate void MatchLiveUpdate();
    public delegate void MatchLiveEnter();
    public delegate void MatchLiveExit();
    public static event MatchLiveUpdate onMatchLiveUpdate;
    public static event MatchLiveEnter onStateEnter;
    public static event MatchLiveExit onStateExit;
    public static MatchLiveUpdate onGamePaused;
    public static MatchLiveUpdate onGameResume;
    private GameSystem gameSystem;
    
    private void PauseGame(object sender, EventArgs e)
    {
        Debug.Log("GamePaused");
        onGamePaused?.Invoke();
    }
    private void ResumeGame(object sender, EventArgs e)
    {
        Debug.Log("GameResumed");
        onGameResume?.Invoke();
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!gameSystem){gameSystem = animator.GetComponent<GameSystem>();}
        onStateEnter?.Invoke();
        PlayerInputHandling.onMenuPressed += PauseGame;
        UIInputHandling.onReturnPressed += ResumeGame;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // tick the clock in the game session
        gameSystem.gameSession.roundTime -= gameSystem.timeProvider.fixedDeltaTime;
        onMatchLiveUpdate?.Invoke();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       PlayerInputHandling.onMenuPressed -= PauseGame;
       onStateExit?.Invoke();
    }
}
