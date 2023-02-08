using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractAIState
{
    public static string StateName;
    protected float timeSinceLastUpdate;
    // protected AbstractAIState(AiController aiPlayerController)
    // {
    //     // this.aiController = aiController;
    //     selectedSkater = aiPlayerController.selectedSkater;
    //     goaltender = aiPlayerController.goaltender;
    //     opponentSkater = aiPlayerController.opponentSkater;
    //     opponentGoaltender = aiPlayerController.opponentGoaltender;
    //     selectedTeamMember = aiPlayerController.selectedTeamMember;
    //     opponentTeamMember = aiPlayerController.opponentTeamMember;
    //     timeSinceLastUpdate = aiPlayerController.AIUpdateTime;
    // }
    public abstract void OnEnter();
    public abstract void OnExit();
    public virtual void OnUpdate()
    {
        // universal for all AIs
    }
}
