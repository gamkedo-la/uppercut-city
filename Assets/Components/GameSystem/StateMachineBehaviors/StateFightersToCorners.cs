using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFightersToCorners : StateMachineBehaviour
{
    public delegate void BetweenRoundEvent(float countDownTime);
    public static GameSystem.GameSystemEvent onStateEnter;
    public static BetweenRoundEvent onBetweenRoundUpdate;
    private GameSystem gameSystem;
    private float entryTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!gameSystem){gameSystem = animator.GetComponent<GameSystem>();}
        entryTime = gameSystem.timeProvider.time;
        onStateEnter?.Invoke();
        InputNeutralHandling.onRequestSkip += SkipCountdown;
        // Longterm: AI takes over, navigates to corner, sits down
    }
    private void SkipCountdown()
    {
        entryTime = gameSystem.timeProvider.time - gameSystem.gameSession.restTime;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Countdown and then go to live match state
        if(gameSystem.timeProvider.time - entryTime >= gameSystem.gameSession.restTime)
        {
            // Transition to next round
            animator.SetBool("FightersToCorner", false);
            animator.SetBool("MatchLive", true);
        }
        onBetweenRoundUpdate?.Invoke(gameSystem.gameSession.restTime - (gameSystem.timeProvider.time - entryTime));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        InputNeutralHandling.onRequestSkip -= SkipCountdown;
    }
}
