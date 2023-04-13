using System;
using UnityEngine;
public class Smb_MatchLive : StateMachineBehaviour
{
    public delegate void MatchLiveUpdate();
    public delegate void MatchLiveEnter();
    public delegate void MatchLiveEvent();
    public static event MatchLiveUpdate onMatchLiveUpdate;
    public static event MatchLiveEnter onStateEnter;
    public static event MatchLiveEvent onStateExit;
    public static MatchLiveEvent onGamePaused;
    public static MatchLiveEvent onGameResume;
    public static MatchLiveEvent onZeroTime;
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
        gameSystem.gameSession.currentRound++;
        onStateEnter?.Invoke();
        PlayerInputHandling.onMenuPressed += PauseGame;
        UIInputHandling.onReturnPressed += ResumeGame;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // tick the clock in the game session
        gameSystem.gameSession.roundTime -= gameSystem.timeProvider.fixedDeltaTime;
        if(gameSystem.gameSession.roundTime <= 0)
        {
            animator.SetBool("MatchLive", false);
            onZeroTime?.Invoke();
        }
        onMatchLiveUpdate?.Invoke();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       PlayerInputHandling.onMenuPressed -= PauseGame;
       onStateExit?.Invoke();
    }
}
