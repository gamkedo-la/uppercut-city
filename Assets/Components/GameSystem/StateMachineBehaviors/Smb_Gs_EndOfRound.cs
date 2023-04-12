using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smb_Gs_EndOfRound : StateMachineBehaviour
{
    private GameSystem gameSystem;
    // is it the last round?
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!gameSystem){gameSystem = animator.GetComponent<GameSystem>();}
        gameSystem.ResetRoundTime();
        if(gameSystem.gameSession.currentRound >= gameSystem.gameSession.totalRounds)
        {
            // end of game
            animator.SetBool("EndOfMatch", true);
        }
        else
        {
            gameSystem.gameSession.currentRound++;
            gameSystem.gameSession.roundTime = gameSystem.gameType.roundTime;
            animator.SetBool("FightersToCorner", true);
        }
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
