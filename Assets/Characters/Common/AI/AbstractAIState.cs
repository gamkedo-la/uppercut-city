using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractAIState
{
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
