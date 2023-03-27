using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractAIState
{
    // Use this class to handle the character's perception of the world
    // Notice the other player
    // Make choices based on our own stats (health, stamina, etc) 
    public static string StateName;
    protected float timeSinceLastUpdate;
    protected AbstractAIState(AIController aiPlayerController)
    {
        // Get references to everything we need
    }
    public abstract void OnEnter();
    public abstract void OnExit();
    public virtual void OnUpdate()
    {
        // 
    }
}
